import { ScrollDispatcher } from '@angular/cdk/scrolling';
import { DOCUMENT } from '@angular/common';
import {
  AfterViewInit,
  Component,
  HostListener,
  Inject,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Router } from '@angular/router';
import 'leader-line';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';

declare let LeaderLine: any;

@Component({
  selector: 'app-training-map-design',
  templateUrl: './training-map-design.component.html',
  styleUrls: ['./training-map-design.component.scss'],
})
export class TrainingMapDesignComponent implements OnInit, AfterViewInit, OnDestroy {
  clicked: boolean;
  CardType: string;
  lines: any[] = [];
  nodes: TrainingMapBox[] = [];
  category: Categories[] = [];
  certifications: any[] = [];
  stickyPoint!: HTMLElement | null;
  mat_cards: Element[] = [];
  connectMode: boolean = false;
  line: any;
  refNode!: HTMLElement | null;
  previousNodeID: string;
  previousLine: any;
  previousLineID: any;
  ila: ILA[] = [];

  public selectedCategory = { title: 'Assessment', iconName: 'bookmark_added' };

  public selectedCertification = { title: 'NERC' };

  compareFn(f1: Categories, f2: Categories): boolean {
    return f1 && f2 ? f1.title === f2.title : f1 === f2;
  }

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private databroadcastSrvc: DataBroadcastService,
    private router: Router,
    private scrollDispatcher: ScrollDispatcher
  ) {
    this.scrollDispatcher.scrolled().subscribe((x) => this.redrawLines());
  }

  ngOnDestroy(): void {
    this.lines.forEach((line) => {
      line.data.remove();
    });
    this.lines = [];
  }

  ngOnInit(): void {
    //Initial Nodes

    this.nodes = [
      {
        title: 'Position',
        trainingProgramType: 'Initial Training',
        iconName: 'business_center',
        id: 'dn_1',
        text: 'First Node',
        nodeType: 'position',
        disabled: false,
        remove: false,
      },
    ];

    this.category = [
      { title: 'Assessment', iconName: 'bookmark_added' },
      { title: 'Individual Development Plan', iconName: 'business_center' },
      { title: 'Certification Requirement', iconName: 'workspace_premium' },
      { title: 'Add Training Group/Module', iconName: 'business_center' },
      { title: 'Position/Label Replacement', iconName: 'business_center' },
      { title: 'ILA', iconName: 'business_center' },
      { title: 'Position Qualification', iconName: 'business_center' },
      { title: 'Timeline', iconName: 'timer' },
    ];

    this.certifications = [
      { name: 'NERC' },
      { name: 'PJM' },
      { name: 'Forklift' },
    ];

    //ila dropdown list
    this.ila = [
      {
        id: 1,
        title: 'ILA 1',
        iconName: 'person',
        description: '1.0 Intro to PowerCo. LO Operation',
      },
      {
        id: 2,
        title: 'ILA 2',
        iconName: 'person',
        description: '2.0 Intro to PowerCo. LO Fundamentals',
      },
      {
        id: 3,
        title: 'ILA 3',
        iconName: 'person',
        description: '3.0 Intro to PowerCo. LO Operation',
      },
      {
        id: 4,
        title: 'ILA 4',
        iconName: 'person',
        description: '4.0 Intro to PowerCo. LO Intermediate I',
      },
      {
        id: 5,
        title: 'ILA 5',
        iconName: 'person',
        description: '5.0 Intro to PowerCo. LO Intermediate II',
      },
      {
        id: 6,
        title: 'ILA 6',
        iconName: 'person',
        description: '6.0 Intro to PowerCo. LO Intermediate III',
      },
    ];
  }

  ngAfterViewInit() {
    this.drawLines();

    this.mat_cards = Array.from(document.querySelectorAll('.card-box'));
    this.stickyPoint = document.getElementById('sticky-point');
  }

  drawLines() {
    for (let i = 0; i < this.nodes.length - 1; i++) {
      let start_point = document.getElementById(this.nodes[i].id);
      let end_point = document.getElementById(this.nodes[i + 1].id);
      let line = this.CreateLeaderLine(start_point, end_point);
      let node_id = this.nodes[i].id + '_' + this.nodes[i + 1].id;
      this.lines.push({ nodeid: node_id, data: line });
    }
    
  }

  dragStarted(e: any) {
    
    // Reposition all the drawn lines
    this.redrawLines();
  }

  redrawLines() {
    
    this.lines.forEach((line) => {
      line.data.position();
    });
  }

  createNode(title: string, refNode: any) {
    
    
    this.nodes[this.nodes.length - 1].remove = true;
    let lastId = this.nodes[this.nodes.length - 1].id.substring(
      this.nodes[this.nodes.length - 1].id.indexOf('_') + 1
    );
    let node_id = 'dn_' + (Number(lastId) + 1);
    let node_text = 'Dynamic Node Coming From ' + refNode;
    let node_title = title;
    let node_icon = 'business_center';
    let node_type = 'simple';
    if (title == '') {
      node_type = 'dropdown';
    }
    this.selectedCategory.title = title;
    this.nodes.push({
      id: node_id,
      text: node_text,
      title: node_title,
      iconName: node_icon,
      nodeType: node_type,
      trainingProgramType: 'Initial Training',
      disabled: true,
      remove: false,
    });
    
    this.previousNodeID = node_id;

    const div = document.querySelector('#div_boundary');

    // Observe the DOM for our newly created Element
    const obsrvr = new MutationObserver((m, o) => {
      let start_point = document.getElementById(refNode);
      let end_point = document.getElementById(node_id);
      //logic for positioning of nodes of even nodes
      
      let line = this.CreateLeaderLine(start_point, end_point);
      this.previousLine = line;

      //positioning of nodes and line removal
      end_point!.style.left = '500px';
      end_point!.style.top = '-200px';
      end_point!.style.position = 'relative';

      this.lines.push({ nodeid: refNode + '_' + node_id, data: line });
      this.previousLineID = refNode + '_' + node_id;
      this.mat_cards = Array.from(document.querySelectorAll('.card-box'));
      this.positionofnodes();
      o.disconnect();
      //this.CardType = 'dropdown';
    });

    if (div) {
      obsrvr.observe(div, {
        childList: true,
        subtree: true,
        attributes: true,
        characterData: true,
      });
    }
  }

  //creating lines in between ILA's
  createILALines() {
    let i = 0;
    let j = 1;
    while (i < this.ila.length && j <= this.ila.length) {
      let inner_div = document.getElementById(this.ila[i].id);
      let inner_div_1 = document.getElementById(this.ila[j].id);
      let line = this.CreateLeaderLine(inner_div, inner_div_1);
      this.lines.push({ nodeid: inner_div + '_' + this.ila[i].id, data: line });
      this.redrawLines();
      if (j % 3 == 0) {
        line.startSocket = 'right';
        line.endSocket = 'top';
        (line.endPlugSize = 1.2),
          (line.startPlugSize = 1.2),
          (line.endSocketGravity = [-30, -30]);
        line.startSocketGravity = [130, 200];
      }
      i++;
      j++;
    }
  }

  //function for styling of add training gorup/module
  AddTraininGroup(add_training: any) {
    add_training!.style.left = '400px';
    add_training!.style.top = '0px';
    add_training!.style.width = '730px';
    add_training!.style.position = 'absolute';
    add_training!.style.backgroundColor = '#fafafa';
  }

  //function for styling of assessment node
  AssessmentNodeStyling(node: any, line: any) {
    line.startSocket = 'bottom';
    line.endSocket = 'top';
    node!.style.top = '600px';
    node!.style.left = '580px';
    node!.style.position = 'absolute';
  }

  createNodeWithTitle(refNode: any, title: string) {
    

    this.selectedCategory.title = title;
    let lastId = this.nodes[this.nodes.length - 1].id.substring(
      this.nodes[this.nodes.length - 1].id.indexOf('_') + 1
    );
    let node_id = 'dn_' + (Number(lastId) + 1);
    let node_text = 'Dynamic Node Coming From ' + refNode;
    let node_title = title;
    let node_icon = 'business_center';
    let node_type = '';
    let rem = false;
    if (title == 'Position/Label Replacement') {
      node_type = 'position';
      node_title = 'Position';
    } else if (title == 'Assessment') {
      node_type = 'assessment';
    } else if (title == 'Individual Development Plan') {
      node_type = 'IDP';
      node_title = 'IDP';
    } else if (title == 'Certification Requirement') {
      node_type = 'Certification';
      node_title = 'Certification';
    } else if (title == 'Add Training Group/Module') {
      node_type = 'Add Training Group/Module';
      node_title = 'Add Training Group/Module';
      rem = true;
    }

    let previousNode = this.nodes[this.nodes.length - 1];
    
    if (previousNode.nodeType == 'dropdown') {
      
      this.nodes.splice(this.nodes.length - 1);
      this.previousLine.remove();

      for (let i = 0; i < this.lines.length; i++) {
        
        let l = this.lines[i].nodeid;
        
        if (l == this.previousLineID) {
          this.lines.splice(i);
        }
      }
      this.previousLine = null;
    }

    this.nodes.push({
      id: node_id,
      text: node_text,
      title: node_title,
      iconName: node_icon,
      nodeType: node_type,
      trainingProgramType: 'Initial Training',
      disabled: false,
      remove: rem,
    });

    //pushing Assessment 1.0 along with Add training Group Module
    if (node_type == 'Add Training Group/Module') {
      this.nodes.push({
        id: node_id + 1,
        text: node_text,
        title: 'Assessment 1.0',
        iconName: node_icon,
        nodeType: 'Assessment 1.0',
        trainingProgramType: 'Initial Training',
        disabled: false,
        remove: rem,
      });
    }

    const card = document.querySelector('#div_boundary');

    //runs the if condition in mutation observer 1 time
    let executed = false;

    // Observe the DOM for our newly created Element
    const obsrvr = new MutationObserver((m, o) => {
      
      //setting the previous node according to 2 nodes pushed at the same time
      if (node_type == 'Add Training Group/Module') {
        previousNode = this.nodes[this.nodes.length - 3];
        
      } else {
        previousNode = this.nodes[this.nodes.length - 2];
        
      }

      let start_point = document.getElementById(previousNode.id);
      
      let end_point = document.getElementById(node_id);

      let line = this.CreateLeaderLine(start_point, end_point);
      this.lines.push({ nodeid: refNode + '_' + node_id, data: line });

      let next_node = this.nodes[this.nodes.length - 2];
      

      if (
        previousNode.nodeType == 'Certification' &&
        next_node.nodeType == 'Add Training Group/Module'
      ) {
        line.endSocket = 'left';
        line.startSocketGravity = [400, 0];
      }

      if (node_type == 'Add Training Group/Module' && executed == false) {
        executed = true;

        // add training group/module node
        let add_training = document.getElementById(node_id);
        

        //assessment node
        let assessment_node = node_id + 1;
        let last_node = document.getElementById(assessment_node);
        

        let line = this.CreateLeaderLine(add_training, last_node);
        this.lines.push({ nodeid: add_training + '_' + node_id, data: line });

        //add training group/module styling
        this.AddTraininGroup(add_training);

        //assessment styling
        this.AssessmentNodeStyling(last_node, line);

        //adding lines to ILA Boxes
        this.createILALines();
        
      }

      
      this.redrawLines();
      this.mat_cards = Array.from(document.querySelectorAll('.card-box'));

      o.disconnect();
    });

    if (card) {
      obsrvr.observe(card, {
        childList: true,
        subtree: true,
        attributes: true,
        characterData: true,
      });
    }
  }

  CreateLeaderLine(
    start_point: any,
    end_point: any,
    isDash = false,
    isDashAnim = false
  ): typeof LeaderLine {
    let primaryColor = 'rgba(6, 78, 59, 1)';
    let animateDash = isDashAnim;

    let ll_options = {
      start: start_point,
      end: end_point,
      ...(isDash && { dash: { animation: animateDash } }),
      path: 'fluid', // fluid
      startPlug: 'disc',
      endPlug: 'arrow1',
      color: primaryColor,
      size: 3,
      startPlugColor: primaryColor,
      startPlugOutline: true,
      startPlugOutlineColor: primaryColor,
      startPlugSize: 1.5,
      endPlugColor: primaryColor,
      endPlugOutline: true,
      endPlugOutlineColor: primaryColor,
      endPlugSize: 1.5,
    };

    return new LeaderLine(ll_options);
  }

  connectNode(refNode: any, event: Event) {
    let start_point = document.getElementById(refNode);
    this.refNode = start_point;
    if (this.connectMode) {
      // if clicked again on the button then Abort
      this.line?.remove();

      if (this.stickyPoint) this.stickyPoint.style.display = 'none';
      this.connectMode = false;
    } else if ((event.target as HTMLElement).parentElement === start_point) {
      // Start
      if (this.stickyPoint) {
        this.stickyPoint.style.display = 'block';

        //hook up our line with the sticky element so line can be moved
        this.line = this.CreateLeaderLine(
          start_point,
          this.stickyPoint,
          true,
          true
        );
      }
      this.connectMode = true;
      this.updateLine(event);
    }
    document.addEventListener('mousemove', (event) => {
      if (this.connectMode) {
        this.updateLine(event);
      }
    });
  }

  updateLine(event: any) {
    if (this.stickyPoint && this.line) {
      this.stickyPoint.style.left = `${event.clientX + scrollX}px`;
      this.stickyPoint.style.top = `${event.clientY + scrollY}px`;
      this.line.position();
    }
  }

  ConnectRefNode(e: Event) {
    let el = e.target as HTMLElement;
    if (el && el.tagName === 'MAT-CARD' && el != this.refNode) {
      const targetNode = this.mat_cards.find((elm) => elm === el);
      const nodeid = this.refNode?.id + '_' + el.id;

      if (targetNode && this.line) {
        if (!this.lines.some((l) => l.nodeid == nodeid)) {
          this.line.setOptions({
            end: targetNode,
            dash: false,
          });

          this.lines.push({
            nodeid: nodeid,
            data: this.line,
          });
        } else {
          this.line.remove();
          alert('already connected');
        }

        this.document.removeEventListener('mousemove', (e) =>
          this.updateLine(e)
        );

        this.line = undefined;
        this.refNode = null;
        this.connectMode = false;
      }
    }
  }

  positionofnodes() {
    

    for (let i = 0; i < this.nodes.length; i++) {
      if (this.nodes[i].nodeType == 'dropdown') {
        let start_point = document.getElementById(this.nodes[i].id);
        if (start_point && this.nodes[i].nodeType == 'dropdown') {
          
          start_point!.style.left = '500px';
          start_point!.style.top = '-200px';
          start_point!.style.position = 'relative';
        }

        //skip one node
        i = i + 1;
        this.redrawLines();
        /*  let end_point = document.getElementById(this.nodes[i + 1].id);
        end_point!.style.marginTop = '-100px';
        end_point!.style.position = 'relative';*/
      }
    }
  }

  async goBack() {
    await this.router.navigate(['dnd/trainingmap']);
  }

  toggleMainMenu() {
    this.databroadcastSrvc.ToggleMainMenu.next('');
    let container = document.querySelector('#div_boundary');
    let obs = new ResizeObserver((entries) => {
      this.redrawLines();
    });
    if (container) obs.observe(container);
  }
}

export class TrainingMapBox {
  title: string;
  trainingProgramType: string;
  iconName: string;
  id!: string;
  text!: string;
  nodeType: string;
  disabled: boolean;
  remove: boolean;
}

export class Categories {
  title: string;
  iconName: string;
}

export class ILA {
  id: any;
  title: string;
  iconName: string;
  description: string;
}

import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Store } from '@ngrx/store';
import { InstructorNavBarMenuItem } from 'src/app/_DtoModels/Instructors/InstructorNavBarMenuItem';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-definitions-navbar',
  templateUrl: './definitions-navbar.component.html',
  styleUrls: ['./definitions-navbar.component.scss']
})
export class DefinitionsNavbarComponent implements OnInit {
  filter = '';
  showActive: boolean = true;
  navList: InstructorNavBarMenuItem[] = [];
  constructor(private store: Store<{ toggle: string }>, private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService, private labelPipe: LabelReplacementPipe,) { }

  ngOnInit(): void {
    this.createList();
  }
  toggleMainMenu() {
    this.store.dispatch(sideBarToggle());
  }
  searchFilter(event: any) {

    
    // this.toFilter = [
    //   ...this.navList.map((n) => {
    //     return {
    //       ...n,
    //       Collapsed: true,
    //       Children: n.Children?.filter((c) =>
    //         c.Title.toLowerCase().includes(String(event).toLowerCase())
    //       ),
    //     };
    //   }),
    // ];
  }
  async createList() {
    let tempArray = ["Techinal Training, Control Center, Electric Operations, System Operations"]

    this.navList = [
      {
        Title: 'QTS',
        RoutePath: '/my-data/definitions/definition-category-details/1',
        disabled:false,
        isVisible:true,
        Children:
        [
          {
            Title: 'Reliability Related',
            RoutePath: '/my-data/definitions/details/1',
            disabled:false,
            isVisible:true
          },
          {
            Title: 'Significant' + await this.transformTitle('Task') +' Change',
            RoutePath: '/my-data/definitions/details/1',
            disabled:false,
            isVisible:true
          },
          {
            Title: 'Annual',
            RoutePath: '/my-data/definitions/details/1',
            disabled:false,
            isVisible:true
          },
        ],

      },
      {
        Title:"Company Specific",
        RoutePath: '/my-data/definitions/definition-category-details/1',
        disabled:false,
        isVisible:true,
        Children:
        [{
          Title: 'Second Annual',
          RoutePath: '/my-data/definitions/details/1',
          disabled:false,
          isVisible:true
        },
        ]
      },
      {
        Title:"NERC",
        RoutePath: '/my-data/definitions/definition-category-details/1',
        disabled:false,
        isVisible:true,
        Children:
        [{
          Title: 'Third Annual',
          RoutePath: '/my-data/definitions/details/1',
          disabled:false,
          isVisible:true
        },
        ]
      }
    ];
  }


  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async openFlyInPanel(templateRef: any) {
    
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  refreshNavBarData() {
    this.navList = [];
    this.createList();
  }

  ngAfterViewInit(): void {
    this.createList();
    // this.subscription.sink =
    //   this.dataBroadcastService.updateMyDataNavBar.subscribe((res: any) => {
    //
    //     this.navList = this.navList.map((data: ProcedureNavBarMenuItem) => {
    //
    //       if (data.id === res.id) {
    //         data.active = !data.active;
    //       }
    //       return data;
    //     });
    //   });

    // this.subscription.sink =
    //   this.dataBroadcastService.updateProcedureInNavBar.subscribe(
    //     (res: any) => {
    //       this.refreshNavBarData();
    //     }
    //   );
    // this.subscription.sink = this.dataBroadcastService.updateProcedureInNavBar.subscribe((res: any) => {
    //   this.refreshNavBarData();
    // });
  }

}

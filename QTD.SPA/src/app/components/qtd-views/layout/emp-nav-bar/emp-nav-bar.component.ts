import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NavBarMenuItem } from 'src/app/_DtoModels/NavBarMenuItem';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { CustomClaimTypes } from 'src/app/_Shared/Utils/CustomClaims';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';

@Component({
  selector: 'app-emp-nav-bar',
  templateUrl: './emp-nav-bar.component.html',
  styleUrls: ['./emp-nav-bar.component.scss']
})
export class EmpNavBarComponent implements OnInit {

  navList: NavBarMenuItem[] = [];
  @Input() htmlContent: any;
  currentRoute: string = '';
  selectedItem: string = '';
  selectedId: any | undefined;

  constructor(private router: Router, private route: ActivatedRoute,
    private licenseHelperService:LicenseHelperService,
    private labelPipe: LabelReplacementPipe,
    ) { }

  showSubMenu: string = '';
  showSideBar: boolean = true;
  ngOnInit(): void {
    this.createList(this);
    this.router.events.subscribe((res) => {

      this.currentRoute = this.router.url;
      var data = this.navList.find((x) => {
        return x.RoutePath === this.currentRoute;
      });
      if (data !== null && data !== undefined) {
        this.selectedId = data.id;
        this.selectedItem = data.Title;

      }
      else {
        this.navList.forEach((x) => {
          if (x.Children !== null && x.Children.length > 0) {
            x.Children.forEach((y) => {
              if (y.RoutePath === this.currentRoute) {
                this.selectedId = y.id;
                this.selectedItem = y.Title;
              }
            })
          }
        })
        //
        // if(children !== null && children !== undefined && children.length > 0){
        //   var childData = children.find((x)=>{
        //     return x.RoutePath === this.currentRoute;
        //   })
        //   if(childData !== null && childData !== undefined){
        //     this.selectedId = childData.id;
        //     this.selectedItem = childData.Title;
        //   }
        // }
      }
      // if(this.currentRoute.trim().toLocaleLowerCase().includes('dashboard')){
      //   this.selectedItem = "Dashboard";
      //   this.selectedId = '1';
      // }
      // else if(this.currentRoute.trim().toLocaleLowerCase().includes('online')){
      //   this.selectedItem = "Online Courses";
      //   this.selectedId = '2';
      // }
      // else if(this.currentRoute.trim().toLocaleLowerCase().includes('regis')){
      //   this.selectedItem = "Self Registration";
      //   this.selectedId = '3';
      // }
      // else if(this.currentRoute.trim().toLocaleLowerCase().includes('test')){
      //   this.selectedItem = "Tests";
      //   this.selectedId = '4';
      // }
      // else if(this.currentRoute.trim().toLocaleLowerCase().includes('qualif')){
      //   this.selectedItem = "Task & Skill Qualifications";
      //   this.selectedId = '5';
      // }
      // else if(this.currentRoute.trim().toLocaleLowerCase().includes('evaluation')){
      //   this.selectedItem = "Student Evaluations";
      //   this.selectedId = '6';
      // }
      // else if(this.currentRoute.trim().toLocaleLowerCase().includes('procedure')){
      //   this.selectedItem = "Procedure Review";
      //   this.selectedId = '1.1';
      // }
      // else if(this.currentRoute.trim().toLocaleLowerCase().includes('report')){
      //   this.selectedItem = "Reports";
      //   this.selectedId = '8'
      // }
    })
    // this.currentRoute=this.route.url[0].path
    //
    //this.route.navigate(['/implementation/test/overview'])
  }

  toggleMenu(name: string) {
    if (this.showSubMenu == name) this.showSubMenu = '';
    else this.showSubMenu = name;
  }

  toggleSideBar(title: string) {
    if (title == 'Job and Task Analysis') {
      let instance =
        jwtAuthHelper.unPackJWTToken[CustomClaimTypes.InstanceName];
      if (instance) {
        this.showSideBar = !this.showSideBar;
        //this.DataBroadcastService.ShowMenuSideBar.next(this.showSideBar);
        //this.jtaSideBarOpen = true;
      }
    }
  }

  async createList(ref?: any) {
    this.navList = [
      {
        Title: 'Dashboard',
        IconName: 'speed',
        RoutePath: '/emp/dashboard',
        id: '1',
        disabled: false,
        hasHover: true,
        Children: null,
        isVisible: true,
      },
      {
        Title: 'Tests',
        IconName: 'assignment',
        RoutePath: '/implementation/test/overview',
        id: '4',
        disabled: false,
        hasHover: true,
        Children: null,
        isVisible: true,
      },
      {
        Title: 'Online Courses',
        IconName: 'speed',
        RoutePath: '/emp/online-courses',
        id: '2',
        disabled: false,
        hasHover: true,
        Children: null,
        isVisible: true,
      },
      {
        Title: 'Student Evaluations',
        IconName: 'bar_chart',
        id: '6',
        RoutePath: '/emp/evaluation',
        disabled: false,
        hasHover: true,
        Children: null,
        isVisible: true,
      },
      {
        Title: 'Task & Skill Qualifications',
        IconName: 'bookmark_border',
        RoutePath: '/emp/task-re-qualification/overview',
        id: '5',
        disabled: false,
        hasHover: true,
        Children: null,
        isVisible: true,
      },
      {
        Title: 'Reviews',
        IconName: 'description',
        RoutePath: null,
        id: '8',
        disabled: false,
        hasHover: false,
        isVisible: true,
        Children: [
          {
            Title: 'IDP Review(Coming Soon)',
            IconName: 'insert_comment',
            RoutePath: null,
            id: '1.2',
            disabled: true,
            hasHover: false,
            Children: null,
            isVisible: true,
          },
          await this.getProcedureReviewMenuItem(),
        ]
      },
      {
        Title: 'Surveys',
        IconName: 'description',
        RoutePath: null,
        id: '19',
        disabled: false,
        hasHover: false,
        isVisible:true,
        Children: [
          {
            Title: 'DIF',
            IconName: '',
            RoutePath: '/emp/dif-survey/overview',
            id: '19.2',
            disabled: false,
            hasHover: false,
            Children: null,
            isVisible:true
          },
          {
            Title: 'Gap',
            IconName: 'insert_comment',
            RoutePath: null,
            id: '19.3',
            disabled: true,
            hasHover: false,
            Children: null,
            isVisible:true
          },
        ]
      },
      {
        Title: 'Self Registration',
        IconName: 'calendar_today',
        RoutePath: '/emp/self-registration',
        id: '3',
        disabled: false,
        hasHover: true,
        Children: null,
        isVisible:true
      },

      {
        Title: 'Reports(Coming Soon)',
        IconName: 'insert_chart',
        RoutePath: '#',
        id: '7',
        disabled: true,
        hasHover: false,
        Children: null,
        isVisible:true
      }
    ];
  }

  async getProcedureReviewMenuItem(){
    var menuItem={ Title: await this.transformTitle('Procedure') + ' Review',IconName: 'insert_comment',RoutePath: '/emp/procedure-review/overview',id: '1.1',disabled: false,hasHover: true,Children: null,tooltip:'', isVisible:true}
    var license = this.licenseHelperService.getLicenseData();
    if (license) {
      if(!license.deluxe){
        menuItem.Title = await this.transformTitle('Procedure') + " Review (Delux Users Only)";
        menuItem.RoutePath = '#';
        menuItem.disabled =true;
        menuItem.hasHover=false;
        menuItem.tooltip ="In order to access this feature your license must support EMP and be a Delux License.  Please contact your account administrator for access"
      }
    }
    return menuItem;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

}

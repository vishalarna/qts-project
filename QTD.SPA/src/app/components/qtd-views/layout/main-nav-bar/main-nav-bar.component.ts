import {ChangeDetectorRef, Component, Input, OnInit} from '@angular/core';
import {Title} from '@angular/platform-browser';
import {Router} from '@angular/router';
import {Store} from '@ngrx/store';
import {take} from 'rxjs/operators';
import {NavBarMenuItem} from 'src/app/_DtoModels/NavBarMenuItem';
import {LabelReplacementPipe} from 'src/app/_Pipes/label-replacement.pipe';
import {
  ReportSkeletonCategoriesService
} from 'src/app/_Services/QTD/ReportSkeletonsCategories/report-skeleton-categories.service';
import {CustomClaimTypes} from 'src/app/_Shared/Utils/CustomClaims';
import {jwtAuthHelper} from 'src/app/_Shared/Utils/jwtauth.helper';
import {freezeMenu} from 'src/app/_Statemanagement/action/state.menutoggle';
import {SubSink} from 'subsink';
import {DataBroadcastService} from 'src/app/_Shared/services/DataBroadcast.service';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';

@Component({
  selector: 'app-main-nav-bar',
  templateUrl: './main-nav-bar.component.html',
  styleUrls: ['./main-nav-bar.component.scss'],
})
export class MainNavBarComponent implements OnInit {
  navList: NavBarMenuItem[] = [];
  @Input() htmlContent: any;
  subscriptions = new SubSink();
  freezeToggle: boolean = false;
  disabledFeatureDataList:any[];
  nercChildren:NavBarMenuItem[];

  constructor(
    private route: Router,
    private labelPipe: LabelReplacementPipe,
    private DataBroadcastService: DataBroadcastService,
    private store: Store<any>,
    private changeDetection: ChangeDetectorRef,
    private reportSkeletonCategoriesService: ReportSkeletonCategoriesService,
    private licenseHelperService:LicenseHelperService,
    private clientSettingsService: ApiClientSettingsService,
    ) {
  }

  showSubMenu: string = '';
  showSideBar: boolean = true;

  ngOnInit(): void {
    this.nercChildren = [];
    this.disabledFeatureDataList = [];
    this.getDisabledFeatureList();    
    this.createList(this);
    this.subscriptions.sink = this.store.select('freezeMenu').subscribe((data: boolean) => {
      this.freezeToggle = data;
    })
    this.subscriptions.sink = this.DataBroadcastService.isUserLoggedIn.subscribe((res) => {
      this.createList(this);
    });
  }

  async getDisabledFeatureList(){
    if(!jwtAuthHelper.SelectedInstance)
    {
      this.nercChildren = [];
      this.nercChildren.push({Title: 'RSAW (Coming Soon)', RoutePath: '#', id: '6.1', disabled: true, isVisible: true});
      this.nercChildren.push({Title: 'CEH Upload', RoutePath: '/nerc/ceh-upload', id: '6.2', disabled:false, hasHover: true, isVisible:true})
    } 
    else{
      this.disabledFeatureDataList = await this.clientSettingsService.getAllFeatureAsync();
      this.getNercChildren();
    }
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

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async getReportSkeletonCategoriesAsync(): Promise<NavBarMenuItem[]> {
    let navbarMenuItems: NavBarMenuItem[] = [];
    if (jwtAuthHelper.SelectedInstance) {
      let reportSkeletonCategoriesList = await this.reportSkeletonCategoriesService.getActiveReportSkeletonCategoriesAsync();
      reportSkeletonCategoriesList.forEach((s, index) => navbarMenuItems.push({
        Title: s.name,
        RoutePath: `/reports`,
        RouteParams: {reportSkeletonCategoryId: s.id},
        id: `10.${index + 2}`,
        disabled: false,
        hasHover: true,
        isVisible: true
      }));
    }
    return navbarMenuItems;
  }

  async createList(ref?: any) {
    this.navList = [
      {
        Title: 'Dashboard',
        IconName: 'speed',
        RoutePath: '/home/dashboard',
        id: '1',
        disabled: false,
        isVisible:true
      },
      {
        Title: await this.transformTitle('My Data'),
        IconName: 'person',
        RoutePath: '',
        id: '1.1',
        disabled: false,
        isVisible:true,
        Children: [
          {
            Title: await this.transformTitle('Employee') + 's',
            RoutePath: '/implementation/employees',
            id: '1.1.1',
            disabled: false,
            hasHover: true,
            isVisible:true
          },
          {
            Title: await this.transformTitle('Position') + 's',
            RoutePath: '/my-data/positions/overview',
            id: '1.1.2',
            disabled: false,
            hasHover: true,
            isVisible:true
          }, //'/my-data/positions/overview'

          {
            Title: await this.transformTitle('Certification') +'s',
            RoutePath: '/my-data/certifications/overview',
            id: '1.1.3',
            disabled: false,
            hasHover: true,
            isVisible:true
          },

          {
            Title: await this.transformTitle('Task') + 's',
            RoutePath: '/my-data/tasks/overview',
            id: '1.1.4',
            disabled: false,
            hasHover: true,
            isVisible:true
          },
          {
            Title: await this.transformTitle('Enabling Objective') + 's',
            RoutePath: '/my-data/enabling-objectives/overview',
            id: '1.1.5',
            disabled: false,
            hasHover: true,
            isVisible:true
          },
          {
            Title: await this.transformTitle('Procedure')+'s',
            RoutePath: '/my-data/procedures/overview',
            id: '1.1.6',
            disabled: false,
            hasHover: true,
            isVisible:true
          },
          {
            Title: await this.transformTitle('Safety Hazard') + 's',
            RoutePath: '/my-data/safety-hazards/overview',
            id: '1.1.7', disabled: false, hasHover: true, isVisible:true
          },
          {Title:  await this.transformTitle('Tool') + 's',
           RoutePath: '/my-data/tools/overview', 
           id: '1.1.8', disabled: false, isVisible:true
          },
          {
            Title: await this.transformTitle('Regulatory Requirement') +'s',
            RoutePath: '/my-data/reg-requirements/overview',
            id: '1.1.9', disabled: false, hasHover: true, isVisible:true
          },
          {
            Title: await this.transformTitle('Definition') + 's (Coming Soon)',
            RoutePath: '/my-data/definitions/overview',
            id: '1.1.10',
            disabled: true,
            hasHover: true,
            isVisible:true
          },
          // { Title: 'Training Locations', RoutePath: '' },
          {
            Title:  await this.transformTitle('Instructor') + 's',
            RoutePath: '/my-data/instructors/overview',
            id: '1.1.11',
            disabled: false,
            hasHover: true,
            isVisible:true
          },
          {
            Title: await this.transformTitle('Location') + 's',
            RoutePath: '/my-data/locations/overview',
            id: '1.1.12',
            disabled: false,
            hasHover: true,
            isVisible:true
          },
          // { Title:'Employee Portal', IconName:'', RoutePath:"", id:'9', Children:[  ] }
        ],
      },
      {
        Title: 'Analysis',
        IconName: 'rocket_launch',
        RoutePath: '',
        id: '2', disabled: false,
        isVisible:true,
        Children: [
          {
            Title: 'Job '+ await this.transformTitle('Task') +' Analysis (Coming Soon)',
            RoutePath: '#', // '/analysis/jta/task-detail',
            id: '2.1',
            disabled: true,
            isVisible:true
          },
          {
            Title: 'RRT Analysis Matrix (Coming Soon)',
            RoutePath: '#',
            id: '2.2',
            disabled: true,
            isVisible:true
          },
          {Title: 'Significant' + await this.transformTitle('Task') +'Change (Coming Soon)', RoutePath: '#', id: '2.3', disabled: true,isVisible:true},
          {Title: 'DIF Surveys', RoutePath: '/analysis/dif-survey/overview', disabled: false,isVisible:true},
          {Title: 'Competency Gap Analysis (Coming Soon)', RoutePath: '#', id: '2.4', disabled: true,isVisible:true},
        ],
      },
      {
        Title: 'Design & Development',
        IconName: 'speed',
        HasChildren: true,
        RoutePath: '',
        disabled: false,
        id: '3',
        isVisible:true,
        Children: [
          {Title: 'Providers and  '+ await this.transformTitle('ILA') +'s', RoutePath: 'dnd/ila', id: '3.2', disabled: false, hasHover: true,isVisible:true},
          {
            Title: 'Tests',
            RoutePath: '',
            HasChildren: true,
            hasHover: true,
            id: '3.3', disabled: false,
            isVisible:true,
            Children: [
              {
                Title: 'Test Question Bank',
                RoutePath: '/dnd/tests/questions',
                id: '3.3.1',
                HasChildren: false,
                hasHover: true, disabled: false,isVisible:true
              },
              {
                Title: 'Design Tests',
                RoutePath: '/dnd/tests',
                id: '3.3.2',
                HasChildren: false,
                hasHover: true, disabled: false,
                isVisible:true
              }
            ]
          },
          {Title: 'Training Programs', RoutePath: 'dnd/trainingprogram', disabled: false, hasHover: true,isVisible:true},
          {Title: 'Training Programs & Map', RoutePath: 'dnd/trainingmap', id: '3.1', disabled: true,isVisible:true},
          {Title: 'Simulator Scenarios', RoutePath: 'dnd/simulatorscenarios',id: '3.4', disabled: false, hasHover: true,isVisible:true},

          //{ Title: 'Simulator Scenarios', RoutePath: '#',id:'3.4', disabled:true },
          //{ Title: 'OJT Guides', RoutePath: '#',id:'3.5', disabled:true },
          //{ Title: 'Task Qualifications', RoutePath: '#',id:'3.6', disabled:true },
          //{ Title: 'Skill Training Guides', RoutePath: '#',id:'3.7', disabled:true },
          //{ Title: 'Skill Assessments', RoutePath: '#',id:'3.8', disabled:true },
          //{ Title: 'Self-Study Guides', RoutePath: '#',id:'3.9', disabled:true },
          //{ Title: 'Enabling Objective Worksheet', RoutePath: '#',id:'3.10', disabled:true },
        ],
      },
      {
        Title: 'Implementation',
        IconName: 'rocket_launch',
        id: '4',
        RoutePath: '',
        disabled: false,
        isVisible:true,
        Children: [
          {Title: 'Scheduling Classes', RoutePath: 'implementation/sc', id: '4.1', disabled: false, hasHover: true,isVisible:true},
          // { Title: 'Roosters/Enrollment', RoutePath: '#',id:'4.2' },
          await this.getProcedureReviewMenuItem(),
          {Title: 'IDP Review (Coming Soon)', RoutePath: '#', id: '4.3', disabled: true,isVisible:true},
          {
            Title: await this.transformTitle('Task') + ' & Skill Qualification',
            RoutePath: 'implementation/taskReQualification',
            id: '4.4',
            disabled: false,
            hasHover: true,
            isVisible:true
          },
        ],
      },
      {
        Title: 'Evaluation',
        IconName: 'insert_chart',
        RoutePath: '',
        id: '5',
        disabled: false,
        isVisible:true,
        Children: [
          {Title: 'Student Evaluations', RoutePath: 'evaluation/studentevaluation', disabled: false, hasHover: true,isVisible:true},
          {
            Title: 'Training Program Review',
            RoutePath: 'evaluation/trainingprogram-review/overview',
            disabled: false,
            hasHover: true,
            isVisible:true
          },
          {Title: await this.transformTitle('Task') + ' List Review', RoutePath: '/evaluation/task-list-review/overview', disabled: false,isVisible:true},
          {Title: 'Test Review/Item Analysis (Coming Soon)', RoutePath: '#', disabled: true,isVisible:true},
          {Title: 'Training Issues', RoutePath: '/evaluation/training-issues/overview', disabled: false,isVisible:true},
        ],
      },
      {
        Title: 'Nerc',
        IconName: 'insert_chart',
        RoutePath: '',
        id: '6',
        disabled: false,
        Children:this.nercChildren,
        isVisible:this.getNercVisiblity()
      },
      {
        Title: 'Templates and Forms',
        IconName: '',
        RoutePath: '',
        id: '7',
        disabled: false,
        isVisible:true,
        Children: [
          {Title: 'Coversheets (Coming Soon)', RoutePath: '#', id: '7.1', disabled: true,isVisible:true},
          {Title: 'Email Notifications', RoutePath: '/templates/notifications/email', disabled: false, hasHover: true,isVisible:true},
        ],
      },
      {
        Title: 'Data Exchange',
        IconName: '',
        RoutePath: '',
        id: '8',
        disabled: false,
        isVisible:true,
        Children: [
          {
            Title: 'Database Settings',
            RoutePath: '/data-exchange/database',
            id: '8.1',
            disabled: false,
            hasHover: true,
            isVisible:true
          },
          {
            Title: 'Data Import', 
            RoutePath: '/data-exchange/import', 
            id: '8.2', 
            disabled: false, 
            HasChildren: true,
            isVisible:true,
         
          },
          {Title: 'Data Export (Coming Soon)', RoutePath: '#', id: '8.3', disabled: true, hasHover: true,isVisible:true},
          {Title: 'APIs (SOS/NWPP) (Coming Soon)', RoutePath: '#', id: '8.4', disabled: true, hasHover: true,isVisible:true},
        ],
      },
      {
        Title: 'Reports',
        IconName: 'insert_chart',
        RoutePath: '',
        id: '10',
        disabled: false, hasHover: true,
        Children: await this.getReportSkeletonCategoriesAsync(),
        isVisible:true
      },
      {
        Title: 'Document Storage',
        IconName: 'folder',
        RoutePath: '/document-storage',
        id: '11',
        disabled: false, hasHover: true,
        isVisible:true
      }
    ];    
  }

  getNercChildren(){
    if(this.disabledFeatureDataList.find(item=>item.feature=="RSAW")?.enabled){
      this.nercChildren.push({Title: 'RSAW (Coming Soon)', RoutePath: '#', id: '6.1', disabled: true,isVisible:true})
    }
    if(this.disabledFeatureDataList.find(item=>item.feature=="CEH Upload")?.enabled){
      this.nercChildren.push({Title: 'CEH Upload', RoutePath: '/nerc/ceh-upload', id: '6.2', disabled:false, hasHover: true,isVisible:true})
    }
  }

  getNercVisiblity(){
    if(this.nercChildren.length<1){
      return false;
    }else{
      return true;
    }
  }

  async changeMenuFreeze(event: any) {
    // this.store.select('menubackdrop').pipe(take(1)).subscribe((data:boolean)=>{
    //   this.store.dispatch(freezeMenu({ doFreeze: event.checked,hasBackDrop:data}));
    // });

    this.store.dispatch(freezeMenu({doFreeze: event.checked}));

  }
  async getProcedureReviewMenuItem(){
    var menuItem={Title: await this.transformTitle('Procedure') + ' Review', RoutePath: '/procedure/overview', id: '9.1', disabled: false, hasHover: true,tooltip:'',isVisible:true}
    var license = this.licenseHelperService.getLicenseData();
    if (license) {
      if(!license.deluxe || !license.hasEmp){
        menuItem.Title = await this.transformTitle('Procedure') + " Review (Delux Users Only)";
        menuItem.RoutePath = '#';
        menuItem.disabled =true;
        menuItem.hasHover=false;
        menuItem.tooltip ="In order to access this feature your license must support EMP and be a Delux License.  Please contact your account administrator for access"
      }
    }
    return menuItem;
  }
}

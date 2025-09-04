import { Component, OnInit } from '@angular/core';
import { NavBarMenuItem } from '@models/NavBarMenuItem';
import { Store } from '@ngrx/store';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  freezeToggle: boolean = false;
  subscriptions = new SubSink();
  navList: NavBarMenuItem[] = [];
  constructor(
    private DataBroadcastService: DataBroadcastService,
    private store: Store<any>
  ) {}

  ngOnInit(): void {
    this.createList(this);
    this.subscriptions.sink = this.store
      .select('freezeMenu')
      .subscribe((data: boolean) => {
        this.freezeToggle = data;
      });
    this.subscriptions.sink =
      this.DataBroadcastService.isUserLoggedIn.subscribe((res) => {
        this.createList(this);
      });
  }

  async createList(ref?: any) {
    this.navList = [
      {
        Title: 'Select Instance',
        IconName: '',
        RoutePath: 'home/instance-selection',
        id: '1',
        disabled: false,
        isVisible: true,
        hasHover: true,
      },
      {
        Title: 'Admin',
        IconName: 'person',
        RoutePath: "",
        id: '1.1',
        disabled: false,
        hasHover: true,
        isVisible: this.isAdminVisible(),
        Children: [
          {
            Title: 'Instance Setup',
            RoutePath: 'admin/instance-setup',
            id: '1.1.1',
            disabled: false,
            hasHover: true,
            isVisible: true,
          },
          {
            Title: 'Message',
            RoutePath: 'admin/admin-messages',
            id: '1.1.2',
            disabled: false,
            hasHover: true,
            isVisible: true,
          },
        ],
      },
    ];
  }

  isAdminVisible(){
    if(jwtAuthHelper.IsAdminUser?.toLowerCase() =="true"){
      return true
    }
      return false;
  }
}

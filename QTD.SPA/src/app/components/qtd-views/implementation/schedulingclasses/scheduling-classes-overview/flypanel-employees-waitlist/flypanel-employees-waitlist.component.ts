import { Component, OnInit } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-flypanel-employees-waitlist',
  templateUrl: './flypanel-employees-waitlist.component.html',
  styleUrls: ['./flypanel-employees-waitlist.component.scss']
})
export class FlypanelEmployeesWaitlistComponent implements OnInit {
empList:any=[]
  constructor(private flyPanelSrvc :FlyInPanelService) { }

  ngOnInit(): void {
this.empList.push('Prabhu SampathKumar')
this.empList.push('Cody Fisher')
this.empList.push('Craig Westervelt')
this.empList.push('Aspen Baptistar')
this.empList.push('Kadin Aminoff')
this.empList.push('Lindsey Westervelt')
this.empList.push('Giana Dokidis')

  }
  closeflyPanel() {
     this.flyPanelSrvc.close();
  }

}

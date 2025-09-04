import { Component, EventEmitter, OnInit,Output} from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';


@Component({
  selector: 'app-fly-panel-settings',
  templateUrl: './fly-panel-settings.component.html',
  styleUrls: ['./fly-panel-settings.component.scss'],
})
export class FlyPanelSettingsComponent implements OnInit {
  @Output() deliverymethod = new EventEmitter<any>();

  delivery_method: Delivery_Methods[]=[];
  checked_boxes:any;

  constructor(
    public flyPanelSrvc: FlyInPanelService
    
  ) {}

  ngOnInit(): void {

    this.delivery_method =[
      {id:1,description:'Workshop'},
      {id:2,description:'Seminar'},
      {id:3,description:'Self-Study'},
      {id:4,description:'In-house training'},
      {id:5,description:'Operator Training Simulation'},
      {id:6,description:'Computer Based Training (CBT)'}
    ];
  }

  countCheckBox(e:any){
    this.checked_boxes=this.delivery_method.filter(i=> i.checked == true);
  }


  SaveChanges(){
    
    this.deliverymethod.emit(this.checked_boxes);
    this.flyPanelSrvc.close();
   }

   OnSave(){
    this.flyPanelSrvc.close();
  }

 
}

export class Delivery_Methods{
  id:any;
  description:string;
  checked?:boolean;
}

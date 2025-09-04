import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Location } from 'src/app/_DtoModels/Locations/Location';
import { LocationService } from 'src/app/_Services/QTD/location.service';

@Component({
  selector: 'app-fly-panel-filter-location',
  templateUrl: './fly-panel-filter-location.component.html',
  styleUrls: ['./fly-panel-filter-location.component.scss']
})
export class FlyPanelFilterLocationComponent implements OnInit {
  showSpinner = false;
  location_list: Location[] = [];
  @Output() closed = new EventEmitter<any>();


  instructor_list: any[] = [];
  @Output() idSelected = new EventEmitter<any>();
  @Output() locSelected = new EventEmitter<any>();
  locationForm: UntypedFormGroup = new UntypedFormGroup({
    locationId: new UntypedFormControl('', Validators.required),
  });
  selectedText: string;
  constructor(    private locationSevc: LocationService,) { }

  ngOnInit(): void {
    this.getLocations();
  }


  async getLocations() {
    await this.locationSevc
      .getLocation()
      .then((res) => {
        this.location_list = res;
        this.location_list=this.location_list.filter(x=>x.active===true);
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  closeProvider() {
    this.closed.emit('fp-add-provider-closed');
  }

  selectLocation(){
    this.idSelected.emit(this.locationForm.get('locationId')?.value);

  }
  onOptionSelection(event: any): void {
    
    this.selectedText = event.source.triggerValue;
  }
  selectedLocationName(){
    this.locSelected.emit(this.selectedText);

  }
}

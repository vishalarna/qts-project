import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  UntypedFormArray,
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Route, Router } from '@angular/router';
import { ClientSettings_FeatureVM } from '@models/ClientSettingsFeature/ClientSettings_FeatureVM';
import { Observable } from 'rxjs';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';

@Component({
  selector: 'app-features',
  templateUrl: './features.component.html',
  styleUrls: ['./features.component.scss'],
})
export class FeaturesComponent implements OnInit {
  @Input()
  clientSettings_Features: Array<ClientSettings_FeatureVM>;
  @Output() onFeatureSaveClickedEvent= new EventEmitter();
  @Output() onFeatureUrlClickedEvent = new EventEmitter();
  @Input() publicUrlSetting: Observable<string>;
  featureForm: UntypedFormGroup;
  updateOptions:ClientSettings_FeatureVM[];
  isPublicEnabled: boolean;
  instanceName: string;
  publicUrlLink: string;
  domain: string;
  @Input() publicUrl;
  constructor(private fb: UntypedFormBuilder, private route: Router) {}

  ngOnInit(): void {
    this.updateOptions = [];
    this.getFeatureForm();  
    this.onIsPublicEnabled(); 
    this.createDomainInstanceUrl()   
    this.publicUrlLink = this.publicUrl
  }

  getFeatureForm() {
    this.featureForm = this.fb.group({
      features: this.fb.array([]),
    });
    this.addFeatureFormData();
  }
  get features() {
    return this.featureForm.get('features') as UntypedFormArray;
  }
  addFeatureFormData() {    
    this.clientSettings_Features.forEach((value) => {
      this.features.push(
        this.fb.group({
          id :[value.id],
          feature: [value.feature],
          enabled: [{
            value: value.enabled,
            disabled: value.feature == "Public Classes"
          }, Validators.required],
        })
      );
    });
  }

  changeFeatureEnabled(event: any, index: number) {
    const featuresArray = this.featureForm.get('features') as UntypedFormArray;
    const featureGroup = featuresArray.at(index);
    featureGroup.get('enabled').setValue(event.target.checked);
  }

  saveFeature() {
  this.updateOptions = [];
  const controls = (this.featureForm.get('features') as UntypedFormArray).controls;

  controls.forEach(control => {
    const id = control.get('id').value;
    const feature = control.get('feature').value;
    const enabled = control.get('enabled').value;

    const updateOption: ClientSettings_FeatureVM = {
      id: id,
      feature: feature,
      enabled: enabled
    };

    this.updateOptions.push(updateOption);
  });
    this.onFeatureSaveClickedEvent.emit(this.updateOptions);  
  }
  onLinkClick(){
     const selectedTabIndex = 1;
     this.onFeatureUrlClickedEvent.emit(selectedTabIndex);
  }
  onIsPublicEnabled(){
    const publicGroup = this.clientSettings_Features.find(g =>
      g.feature == 'Public Classes'
    );
    this.isPublicEnabled = publicGroup.enabled;
  }

  getInstanceName(){
    this.instanceName = jwtAuthHelper.SelectedInstance;
  }
  
  createDomainInstanceUrl(){
    var fullPath = window.location.href;
    var path = fullPath.split("/");
    this.domain = path.slice(0,-2).join("/");
    this.publicUrlSetting.subscribe((publicUrl) =>{
      this.publicUrlLink = publicUrl;
    })
    this.getInstanceName();

    
  }
}

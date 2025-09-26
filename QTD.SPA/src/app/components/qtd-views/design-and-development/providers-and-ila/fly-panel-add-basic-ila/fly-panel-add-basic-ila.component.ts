import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { DeliveryMethod } from '@models/DeliveryMethod/DeliveryMethod';
import { ILABasicCreateOptions } from '@models/ILA/ILABasicCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DeliveryMethodeService } from 'src/app/_Services/QTD/delivery-methode.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { Provider } from 'src/app/_DtoModels/Provider/Provider';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { CertificationListVM } from '@models/Certification/CertificationListVM';
import { SubRequirementVM } from '@models/CertifyingBody/CertifyingBodyWithSubRequirementsVM';
import { CertifyingBodiesService } from 'src/app/_Services/QTD/certifying-bodies.service';
import { CEHUpdateOptions } from '@models/ILA/CEHUpdationOptions';

@Component({
  selector: 'app-fly-panel-add-basic-ila',
  templateUrl: './fly-panel-add-basic-ila.component.html',
  styleUrls: ['./fly-panel-add-basic-ila.component.scss']
})
export class FlyPanelAddBasicIlaComponent {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();

  basicIlaForm!: UntypedFormGroup;
  providers: Provider[] = [];
  deliveryMethods: DeliveryMethod[] = [];
  showSpinner = false;
  providerLoader = false;
  creditHoursView:string='ByCertification';
  certifyingBodiesList: any[] = [];
  certificationLoader = true;
  certifyingBodySelected: string = "";
  NercCertificatesList: CertificationListVM[] = [];
  CertificatesList: CertificationListVM[] = [];
  certificationSelected: number | null = null;
  allDeliveryMethods: DeliveryMethod[] = [];
  isNercBit: boolean = false;
  providerSelected = false;
  nercCertificatesFiltered: any[] = [];
  subrequirements: any[];
  savedCertifications: any[] = [];
  certifyingBodySubRequirements: SubRequirementVM[] = [];

  constructor(
    private fb: UntypedFormBuilder,
    private providerSrvc: ProviderService,
    private deliveryMethodSrvc: DeliveryMethodeService,
    private ilaService: IlaService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
    private certService: CertificationService,
    private certBodyService: CertifyingBodiesService,
  ) {}

  ngOnInit(): void {
    this.initializeBasicIlaForm();
    this.getProviders();
    this.fetchAllCertifications();
    this.getDeliveryMethods();  

    this.basicIlaForm.get('providerId')?.valueChanges.subscribe((providerId) => {
      const provider = this.providers.find(p => p.id === providerId);
      if (provider) {
        this.providerSelected = true;
        this.isNercBit = provider.isNERC;
        this.filterDeliveryMethods(provider.isNERC);
        this.basicIlaForm.get('deliveryMethodId')?.enable();
      }else {
        this.providerSelected = false;
        this.isNercBit = false;
        this.basicIlaForm.get('deliveryMethodId')?.disable();
        this.basicIlaForm.get('deliveryMethodId')?.reset();
      }
    });
  }

  initializeBasicIlaForm(): void {
    this.basicIlaForm = this.fb.group({
      providerId: new UntypedFormControl('', [Validators.required]),
      name: new UntypedFormControl('', [Validators.required]),
      number: new UntypedFormControl('', [Validators.required]),
      totalHours: new UntypedFormControl(),
      deliveryMethodId: new UntypedFormControl({ value: null, disabled: true }),
      IsSelfPacedILA: new UntypedFormControl(false),
      addAnother: new UntypedFormControl(false),
      certificationSelected: new UntypedFormControl(null),
      certifyingBodySelected: new UntypedFormControl(null),
      cehHours: new UntypedFormControl(null), 
      includeSimulation: new UntypedFormControl(false),
      emergencyOPHours: new UntypedFormControl(false),
      partialCredit: new UntypedFormControl(false),
    });
  }

  async getProviders() {
    this.providerLoader = true;
    await this.providerSrvc.getWihtoutIncludes().then((res) => {
      this.providers = res;
    }).finally(()=>{
      this.providerLoader = false;
    });
  }

  async getDeliveryMethods() {
   this.deliveryMethodSrvc.getAll().then((res)=>{
    this.allDeliveryMethods = res;
     this.deliveryMethods = res;
   })
  }

  filterDeliveryMethods(providerIsNerc: boolean) {
    this.deliveryMethods = this.allDeliveryMethods.filter(dm => {
      if (providerIsNerc) {
        return dm.isNerc === true || dm.isAvailableForAllIlas === true;
      } else {
        return dm.isNerc === false || dm.isAvailableForAllIlas === true;
      }
    });
  
    const currentDeliveryId = this.basicIlaForm.get('deliveryMethodId')?.value;
    if (currentDeliveryId && !this.deliveryMethods.some(dm => dm.id === currentDeliveryId)) {
      this.basicIlaForm.get('deliveryMethodId')?.reset();
    }
  }

  async saveILA(status: 'Draft' | 'Published') {
    if (this.basicIlaForm.get('providerId')?.invalid ||
      this.basicIlaForm.get('name')?.invalid ||
      this.basicIlaForm.get('number')?.invalid) {
      this.basicIlaForm.markAllAsTouched();
    return;
  }

    this.showSpinner = true;
    const options = new ILABasicCreateOptions();
    options.providerId = this.basicIlaForm.get('providerId')?.value;
    options.name = this.basicIlaForm.get('name')?.value;
    options.number = this.basicIlaForm.get('number')?.value;
    options.totalHours = this.basicIlaForm.get('totalHours')?.value;
    options.deliveryMethodId = this.basicIlaForm.get('deliveryMethodId')?.value;
    options.isSelfPacedILA = this.basicIlaForm.get('IsSelfPacedILA')?.value;
    options.isPublished = status=="Draft"? false : true ;
    options.cehUpdates = this.savedCertifications.map(cert => {
      let subRequirements: SubRequirementVM[] = [];
    
      if (cert.certificationId === this.certificationSelected && this.certifyingBodySubRequirements?.length) {
        subRequirements = this.certifyingBodySubRequirements.map(sub => ({
          subRequirementId: sub.subRequirementId,
          reqName: sub.reqName,
          reqHour: this.basicIlaForm.get(sub.subRequirementId.toString())?.value ?? 0
        }));
      } 
      else if (cert.certifyingBody && cert.certifyingBody === this.certifyingBodySelected && this.subrequirements?.length) {
        subRequirements = this.subrequirements.map(sub => ({
          subRequirementId: sub.subRequirementId,
          reqName: sub.reqName,
          reqHour: this.basicIlaForm.get(sub.subRequirementId.toString())?.value ?? 0
        }));
      } 
      else if (cert.subRequirements?.length) {
        subRequirements = cert.subRequirements.map(sub => ({ ...sub }));
      }
    
      return {
        certificationId: cert.certificationId,
        certificationName: cert.certificationName,
        certifyingBody: cert.certifyingBody,
        cehHours: cert.cehHours ?? 0,
        isIncludeSimulation: cert.isIncludeSimulation || false,
        isEmergencyOpHours: cert.isEmergencyOpHours || false,
        isPartialCreditHours: cert.isPartialCreditHours || false,
        subRequirements
      } as CEHUpdateOptions;
    });

    await this.ilaService.createBasicILA(options)
      .then(async (res) => {
        this.showSpinner = false;
        this.alert.successToast(await this.labelPipe.transform('ILA') + " Saved Successfully");
        this.refresh.emit();

        if (this.basicIlaForm.get('addAnother')?.value) {
          this.basicIlaForm.reset({ addAnother: false });
          this.certificationSelected = null;
          this.certifyingBodySelected = null;
          this.certifyingBodySubRequirements = [];
          this.subrequirements = [];
          this.nercCertificatesFiltered = [];
          this.savedCertifications = [];
        } else {
          this.closed.emit();
        }
      })
      .catch(async (err) => {
        this.showSpinner = false;
        this.alert.errorAlert(
          'Record Already Exists',
          'The ' + await this.labelPipe.transform('ILA') + ' you are trying to create already exists'
        );
      });
  }

  changeCreditHoursView(event) {
    this.creditHoursView=event;
    if (this.creditHoursView === 'ByCertification') {
      this.basicIlaForm.get('certifyingBodySelected')?.reset();
      this.certifyingBodySelected = '';
      this.nercCertificatesFiltered = [];
    } else if (this.creditHoursView === 'ByCertifyingBody') {
      this.basicIlaForm.get('certificationSelected')?.reset();
      this.certificationSelected = null; 
      if (this.certifyingBodySelected === 'NERC') {
        this.nercCertificatesFiltered = [...this.NercCertificatesList];
      }
    }
  }

  async fetchAllCertifications() {
    this.CertificatesList = [];
    this.NercCertificatesList = [];
    this.certificationLoader = true;
  
    try {
      const res = await this.certService.getCertCategoryWithCert();
      const nercBody = res.find((x: any) => x.certifyingBody?.name === "NERC");
      this.certifyingBodiesList.push(nercBody)

      if (nercBody) {
        this.NercCertificatesList = nercBody.certificationCompactOptions.map((certificate: any) => {
          const cer = new CertificationListVM();
          cer.certificationId = certificate.id;
          cer.certificationName = certificate.name;
          cer.IsNerc = true;
          return cer;
        });
      }   
  
      res.forEach((x: any) => {
        x.certificationCompactOptions?.forEach((certificate: any) => {
          if (!this.NercCertificatesList.some(n => n.certificationId === certificate.id)) {
            const cer = new CertificationListVM();
            cer.certificationId = certificate.id;
            cer.certificationName = certificate.name;
            cer.IsNerc = x.certifyingBody?.isNERC ?? false;
            this.CertificatesList.push(cer);
          };
        });
      });
    } catch (err) {
      console.error("Error fetching certifications", err);
    } finally {
      this.certificationLoader = false;
    }
  }
  
  async certificationSelectionChange(event: any) {
    const newCertId = event.value;
  
    this.certificationSelected = newCertId;
  
    this.basicIlaForm.patchValue({
      cehHours: null,
      includeSimulation: false,
      emergencyOPHours: false,
      partialCredit: false
    });
  
    let savedData = this.savedCertifications.find(c => c.certificationId === newCertId);
  
    if (savedData) {
      this.basicIlaForm.patchValue({
        cehHours: savedData.cehHours,
        includeSimulation: savedData.isIncludeSimulation,
        emergencyOPHours: savedData.isEmergencyOpHours,
        partialCredit: savedData.isPartialCreditHours
      });
    }
  
    if (newCertId) {
      try {
        this.certifyingBodySubRequirements = await this.certService.getSubRequirementsByCertId(newCertId);
  
        this.certifyingBodySubRequirements.forEach(sub => {
          const controlName = sub.subRequirementId.toString();
  
          const savedSub = savedData?.subRequirements?.find(s => s.subRequirementId.toString() === sub.subRequirementId.toString());
          const value = savedSub ? savedSub.reqHour : (sub.reqHour || null);
  
          if (!this.basicIlaForm.contains(controlName)) {
            this.basicIlaForm.addControl(controlName, new FormControl(value, Validators.required));
          } else {
            this.basicIlaForm.get(controlName)?.setValue(value);
          }
        });
      } catch (err) {
        console.error("Failed to fetch subrequirements", err);
        this.certifyingBodySubRequirements = [];
      }
    }
  }
  
  
  certifyingBodySelectionChange(event: any) {
    this.certifyingBodySelected = event.value;
    this.subrequirements = [];
  
    const saved = this.savedCertifications.filter(
      b => b.certifyingBody === this.certifyingBodySelected
    );
  
    if (saved.length) {
      this.basicIlaForm.patchValue({
        certifyingBodySelected: saved[0].certifyingBody,
        cehHours: saved[0].cehHours,
        includeSimulation: saved[0].isIncludeSimulation,
        emergencyOPHours: saved[0].isEmergencyOpHours,
        partialCredit: saved[0].isPartialCreditHours
      });
    
      this.nercCertificatesFiltered = saved.map(s => ({
        certificationId: s.certificationId,
        certificationName: s.certificationName,
        IsNerc: true
      }));
    } else {
      this.basicIlaForm.patchValue({
        certifyingBodySelected: this.certifyingBodySelected,
        cehHours: null,
        includeSimulation: false,
        emergencyOPHours: false,
        partialCredit: false
      });
  
      this.nercCertificatesFiltered = [];
    }

    if (this.certifyingBodySelected) {
      this.certBodyService
        .getCertifyingBodiesWithSubRequirementsAsync(true)
        .then(subReqs => {
          this.subrequirements = subReqs;
          subReqs.forEach(sub => {
            if (!this.basicIlaForm.contains(sub.subRequirementId.toString())) {
              this.basicIlaForm.addControl(
                sub.subRequirementId.toString(),
                new UntypedFormControl(sub.reqHour, Validators.required)
              );
            }
          });
        })
        .catch(err => console.error('Failed to load subrequirements', err));
    }
    if (event.value === 'NERC' && saved.length === 0) {
      const nercBody = this.certifyingBodiesList.find(
        x => x.certifyingBody?.name === 'NERC'
      );
  
      this.nercCertificatesFiltered = nercBody?.certificationCompactOptions.map((c: any) => ({
        certificationId: c.id,
        certificationName: c.name,
        IsNerc: true
      })) || [];
    }
  }
  
  saveByCertifications() {
    const selectedCertId = this.certificationSelected?.toString();
    const selectedCert = this.CertificatesList.find(c => c.certificationId.toString() === selectedCertId);

    const subRequirements: SubRequirementVM[] = [];
    if (this.certifyingBodySubRequirements?.length) {
      this.certifyingBodySubRequirements.forEach(sub => {
        subRequirements.push({
          subRequirementId: sub.subRequirementId,
          reqName: sub.reqName,
          reqHour: this.basicIlaForm.get(sub.subRequirementId.toString())?.value || 0
        });
      });
    }
  
    const certData = {
      certificationId: selectedCertId,
      certificationName: selectedCert ? selectedCert.certificationName : '',
      cehHours: this.basicIlaForm.get('cehHours')?.value,
      isIncludeSimulation: this.basicIlaForm.get('includeSimulation')?.value,
      isEmergencyOpHours: this.basicIlaForm.get('emergencyOPHours')?.value,
      isPartialCreditHours: this.basicIlaForm.get('partialCredit')?.value,
      subRequirements 
    };
  
    const index = this.savedCertifications.findIndex(c => c.certificationId === selectedCertId);
    if (index >= 0) {
      this.savedCertifications[index] = certData;
    } else {
      this.savedCertifications.push(certData);
    }
    this.certificationSelected = null;
    this.certifyingBodySubRequirements = [];
    this.basicIlaForm.patchValue({
      certificationSelected: null,
      cehHours: null,
      includeSimulation: false,
      emergencyOPHours: false,
      partialCredit: false
    });
  }
  
  deleteLinksByCertifications() {
    const selectedCertId = this.certificationSelected;
    this.savedCertifications = this.savedCertifications.filter(c => c.certificationId !== selectedCertId);
    if (this.certifyingBodySubRequirements?.length) {
      this.certifyingBodySubRequirements.forEach(sub => {
        if (this.basicIlaForm.contains(sub.subRequirementId.toString())) {
          this.basicIlaForm.removeControl(sub.subRequirementId.toString());
        }
      });
    }
  
    this.basicIlaForm.patchValue({
      certificationSelected: null,
      cehHours: null,
      includeSimulation: false,
      emergencyOPHours: false,
      partialCredit: false
    });
  
    this.certificationSelected = null;
    this.certifyingBodySubRequirements = [];
  }
  
  saveByCertifyingBody() {
    const bodyName = this.basicIlaForm.get('certifyingBodySelected')?.value;
  
    const subRequirements: SubRequirementVM[] = [];
    if (this.subrequirements?.length) {
      this.subrequirements.forEach(sub => {
        subRequirements.push({
          subRequirementId: sub.subRequirementId,
          reqName: sub.reqName,
          reqHour: this.basicIlaForm.get(sub.subRequirementId.toString())?.value || 0
        });
      });
    }
  
    this.nercCertificatesFiltered.forEach(cert => {
      const certData = {
        certifyingBody: bodyName,
        certificationId: cert.certificationId,
        certificationName: cert.certificationName,
        cehHours: this.basicIlaForm.get('cehHours')?.value,
        isIncludeSimulation: this.basicIlaForm.get('includeSimulation')?.value,
        isEmergencyOpHours: this.basicIlaForm.get('emergencyOPHours')?.value,
        isPartialCreditHours: this.basicIlaForm.get('partialCredit')?.value,
        subRequirements
      };
  
      const index = this.savedCertifications.findIndex(c => c.certificationId === cert.certificationId);
      if (index >= 0) {
        this.savedCertifications[index] = certData;
      } else {
        this.savedCertifications.push(certData);
      }
    });
  
    this.certifyingBodySelected = null;
    this.subrequirements = [];
    this.basicIlaForm.patchValue({
      certifyingBodySelected: null,
      cehHours: null,
      includeSimulation: false,
      emergencyOPHours: false,
      partialCredit: false
    });
  }
  
  
  deleteLinksByCertifyingBody() {
    const bodyName = this.basicIlaForm.get('certifyingBodySelected')?.value;
    this.savedCertifications = this.savedCertifications.filter(b => b.certifyingBody !== bodyName);

    if (this.subrequirements?.length) {
      this.subrequirements.forEach(sub => {
        if (this.basicIlaForm.contains(sub.subRequirementId.toString())) {
          this.basicIlaForm.removeControl(sub.subRequirementId.toString());
        }
      });
    }
  
    this.basicIlaForm.patchValue({
      certifyingBodySelected: null,
      cehHours: null,
      includeSimulation: false,
      emergencyOPHours: false,
      partialCredit: false
    });
  
    this.subrequirements = [];
    this.certifyingBodySelected = null;
    this.nercCertificatesFiltered = [];
  }
  
  isSelectedAndSaved(): boolean {
    if (this.creditHoursView === 'ByCertification') {
      const selectedCertId = this.certificationSelected;
      return !!selectedCertId && this.savedCertifications.some(c => c.certificationId === selectedCertId);
    }
  
    if (this.creditHoursView === 'ByCertifyingBody') {
      const bodyName = this.basicIlaForm.get('certifyingBodySelected')?.value;
      return !!bodyName && this.savedCertifications.some(c => c.certifyingBody === bodyName);
    }
  
    return false;
  }

  closeCertificationForm() {
    this.certificationSelected = null;
    this.certifyingBodySubRequirements = [];
  
    const controlsToReset = ['certificationSelected', 'cehHours', 'includeSimulation', 'emergencyOPHours', 'partialCredit'];
    controlsToReset.forEach(ctrl => {
      if (this.basicIlaForm.contains(ctrl)) {
        this.basicIlaForm.get(ctrl)?.reset(ctrl === 'cehHours' ? null : false);
      }
    });
  
    Object.keys(this.basicIlaForm.controls).forEach(ctrl => {
      if (this.certifyingBodySubRequirements.find(s => s.subRequirementId.toString() === ctrl)) {
        this.basicIlaForm.removeControl(ctrl);
      }
    });
  }
  
  closeCertifyingBodyForm() {
    this.subrequirements?.forEach(sub => {
      if (this.basicIlaForm.contains(sub.subRequirementId.toString())) {
        this.basicIlaForm.removeControl(sub.subRequirementId.toString());
      }
    });
  
    const controlsToReset = ['certifyingBodySelected', 'cehHours', 'includeSimulation', 'emergencyOPHours', 'partialCredit'];
    controlsToReset.forEach(ctrl => {
      if (this.basicIlaForm.contains(ctrl)) {
        this.basicIlaForm.get(ctrl)?.reset(ctrl === 'cehHours' ? null : false);
      }
    });
  
    this.certifyingBodySelected = null;
    this.subrequirements = [];
    this.nercCertificatesFiltered = [];
  }  

  isByCertifyingBodyFormValid(): boolean {
    if (!this.certifyingBodySelected) return false;
  
    const cehValid = (this.basicIlaForm.get('cehHours')?.value ?? 0) > 0;
  
    const subReqValid = this.subrequirements?.every(sub => {
      const val = this.basicIlaForm.get(sub.subRequirementId.toString())?.value ?? 0;
      return val > 0;
    }) ?? true;
  
    return cehValid && subReqValid;
  }
  
  isByCertificationFormValid(): boolean {
    if (!this.certificationSelected) return false;
  
    const cehValid = (this.basicIlaForm.get('cehHours')?.value ?? 0) > 0;
  
    const subReqValid = this.certifyingBodySubRequirements?.every(sub => {
      const val = this.basicIlaForm.get(sub.subRequirementId.toString())?.value ?? 0;
      return val > 0;
    }) ?? true;
  
    return cehValid && subReqValid;
  }  
  
  isMainFormValid(): boolean {
    const baseValid = !!this.basicIlaForm.get('providerId')?.value &&
                      !!this.basicIlaForm.get('name')?.value &&
                      !!this.basicIlaForm.get('number')?.value;
  
    if (!baseValid) return false;
  
    if (this.creditHoursView === 'ByCertification' && this.certificationSelected) {
      return this.isByCertificationFormValid();
    }
  
    if (this.creditHoursView === 'ByCertifyingBody' && this.certifyingBodySelected) {
      return this.isByCertifyingBodyFormValid();
    }
  
    return true;
  } 
  
  keyPressNumbers(event: any) {
    var charCode = event.which ? event.which : event.keyCode;
     if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  toggleSelection(event: Event, id: any) {
    const control = this.basicIlaForm.get('deliveryMethodId');
    if (control?.disabled) {
      event.preventDefault(); 
      return;
    }
    event.preventDefault();
    
    if (control?.value === id) {
      control.setValue(null);
    } else {
      control?.setValue(id);
    }
  }
}

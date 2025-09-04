import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { ILA } from '@models/ILA/ILA';
import { Provider } from '@models/Provider/Provider';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { Position } from '@models/Position/Position';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { SimulatorScenario_FilterVM } from '@models/SimulatorScenarios_New/SimulatorScenario_FilterVM';

@Component({
  selector: 'app-fly-panel-filter-simulator-scenarios',
  templateUrl: './fly-panel-filter-simulator-scenarios.component.html',
  styleUrls: ['./fly-panel-filter-simulator-scenarios.component.scss'],
})
export class FlyPanelFilterSimulatorScenariosComponent implements OnInit {
  @Input() filterSimScenarioValue: SimulatorScenario_FilterVM;
  @ViewChild('posSelect', { static: false }) posSelect!: MatSelect;
  @ViewChild('provSelect', { static: false }) provSelect!: MatSelect;
  @ViewChild('ilaSelect', { static: false }) ilaSelect!: MatSelect;
  @ViewChild('difficultySelect', { static: false })
  difficultySelect!: MatSelect;
  @ViewChild('statusSelect', { static: false }) statusSelect!: MatSelect;
  @ViewChild('activeStatusSelect', { static: false })
  activeStatusSelect!: MatSelect;
  @Output() simScenarioFilterChange = new EventEmitter<any>();
  filterSimulatorScenariosForm: UntypedFormGroup;
  provider_list: Provider[] = [];
  provider_list_original: Provider[] = [];
  ila_list: any[] = [];
  ila_list_original: any[] = [];
  isSpinner: boolean = true;
  isILALoading: boolean = true;
  positions: Position[];
  difficultyLevel: any;
  simulatorScenarioStatuses: any;
  initialPositionData: any[] = [];

  constructor(
    private fb: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    private providerSrvc: ProviderService,
    private ilaService: IlaService,
    private posService: PositionsService,
    private simScenariosService: SimulatorScenariosService
  ) {}

  async ngOnInit() {
    setTimeout(() => {
      this.posSelect._handleKeydown = (event: KeyboardEvent) => {
        if (event.key === 'SPACE') return;
      };
    });
    setTimeout(() => {
      this.provSelect._handleKeydown = (event: KeyboardEvent) => {
        if (event.key === 'SPACE') return;
      };
    });
    setTimeout(() => {
      this.difficultySelect._handleKeydown = (event: KeyboardEvent) => {
        if (event.key === 'SPACE') return;
      };
    });
    setTimeout(() => {
      this.statusSelect._handleKeydown = (event: KeyboardEvent) => {
        if (event.key === 'SPACE') return;
      };
    });
    setTimeout(() => {
      this.activeStatusSelect._handleKeydown = (event: KeyboardEvent) => {
        if (event.key === 'SPACE') return;
      };
    });
    setTimeout(() => {
      this.ilaSelect._handleKeydown = (event: KeyboardEvent) => {
        if (event.key === 'SPACE') return;
      };
    });
    this.initializeSimulatorScenariosForm();
    await this.getProviders();
    await this.getAllActivePositions();
    await this.getAllDifficultyLevel();
    await this.getAllSimulatorScenarioStatus();
  }

  async initializeSimulatorScenariosForm() {  
    this.filterSimulatorScenariosForm = this.fb.group({
      provider: new UntypedFormControl(this.filterSimScenarioValue?.provider),
      position: new UntypedFormControl(this.filterSimScenarioValue?.position),
      ila: new UntypedFormControl(this.filterSimScenarioValue?.ila),
      difficultyLevel: new UntypedFormControl( this.filterSimScenarioValue?.difficultyLevel),
      status: new UntypedFormControl(this.filterSimScenarioValue?.status),
      activeStatus: new UntypedFormControl(this.filterSimScenarioValue?.activeStatus),
      simScenariosNotLinkedToILA: new UntypedFormControl(this.filterSimScenarioValue?.simScenariosNotLinkedToILA),
      searchTxt: new UntypedFormControl(''),
      ilaSearch: new UntypedFormControl(''),
      searchPosition: new UntypedFormControl(''),
    });
  }

  filterData() {
    this.filterSimulatorScenariosForm.get('searchPosition')?.setValue('');
    this.positionSearch();
  }

  handleILAFilters() {
    this.filterSimulatorScenariosForm.get('ilaSearch')?.setValue('');
    this.ilaSearch();
  }

  handleProviderFilters() {
    this.filterSimulatorScenariosForm.get('searchTxt')?.setValue('');
    this.providerSearch();
  }

  closeFlyPanel() {
    this.flyPanelService.close();
  }

  async getAllActivePositions() {
    this.positions = (await this.posService.getAllWithoutIncludes()).filter(
      (x) => x.active
    );
    this.initialPositionData = Object.assign(this.positions);
  }

  async getAllDifficultyLevel() {
    this.difficultyLevel =
      await this.simScenariosService.getAllDifficultyAsync();
  }

  async getAllSimulatorScenarioStatus() {
    this.simulatorScenarioStatuses =
      await this.simScenariosService.getAllSimulatorScenarioStatuses();
  }

  async getProviders() {
    await this.providerSrvc
      .getActiveProviders()
      .then((res: any) => {
        this.provider_list = res;
        this.provider_list_original = Object.assign(this.provider_list);
      })
      .finally(() => {});
      if (this.filterSimScenarioValue?.provider) {
        const selectedProvider = this.provider_list.find(provider => provider.id === this.filterSimScenarioValue.provider);
        if (selectedProvider) {
          await this.selectProvider(selectedProvider.id);
        }
      }
  }

  async selectProvider(event: any) {
    this.ila_list = [];
    this.isILALoading = true;
    await this.ilaService
      .getByProvider(event)
      .then(async (res: any) => {
        this.ila_list = res.filter((x) => x.active);
        this.ila_list_original = Object.assign(this.ila_list);
      })
      .finally(() => {
        this.isILALoading = false;
      });
  }

  getFilterValues() {
    this.filterSimScenarioValue = {
      provider: this.filterSimulatorScenariosForm.get('provider').value,
      position: this.filterSimulatorScenariosForm.get('position').value,
      ila: this.filterSimulatorScenariosForm.get('ila').value,
      difficultyLevel: this.filterSimulatorScenariosForm.get('difficultyLevel').value,
      status: this.filterSimulatorScenariosForm.get('status').value,
      activeStatus: this.filterSimulatorScenariosForm.get('activeStatus').value,
      simScenariosNotLinkedToILA: this.filterSimulatorScenariosForm.get(
        'simScenariosNotLinkedToILA'
      ).value,
    };
  }

  clearProviderSelection() {
    this.filterSimulatorScenariosForm.get('provider').setValue(null);
    this.filterSimulatorScenariosForm.get('ila').setValue(null);
  }

  clearILASelection() {
    this.filterSimulatorScenariosForm.get('ila').setValue(null);
  }

  clearPositionSelection() {
    this.filterSimulatorScenariosForm.get('position').setValue(null);
  }
  clearDifficultyLevelSelection() {
    this.filterSimulatorScenariosForm.get('difficultyLevel').setValue(null);
  }
  clearStatusSelection() {
    this.filterSimulatorScenariosForm.get('status').setValue(null);
  }
  clearActiveStatusSelection() {
    this.filterSimulatorScenariosForm.get('activeStatus').setValue(null);
  }

  providerSearch() {
    var filterString =
      this.filterSimulatorScenariosForm.get('searchTxt')?.value;

    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).toLowerCase();
    } else {
      filterString = '';
    }
    this.provider_list = this.provider_list_original.filter((f) => {
      return f.name.toLowerCase().includes(filterString);
    });
  }

  ilaSearch() {
    var filterString =
      this.filterSimulatorScenariosForm.get('ilaSearch')?.value;
    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).toLowerCase();
    } else {
      filterString = '';
    }
    return (this.ila_list = this.ila_list_original.filter((f) => {
      return (
        (f.name.toLowerCase().includes(filterString) ||
          f.number?.toLowerCase().includes(filterString)) &&
        f.active == true
      );
    }));
  }

  positionSearch() {
    var filterString =
      this.filterSimulatorScenariosForm.get('searchPosition')?.value;
    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).toLowerCase();
    } else {
      filterString = '';
    }
    this.positions = this.initialPositionData.filter((f) => {
      return f.positionTitle.toLowerCase().includes(filterString);
    });
  }

  applyFiltersClick() {
    this.getFilterValues();
    this.simScenarioFilterChange.emit(this.filterSimScenarioValue);
    this.flyPanelService.close();
  }
}

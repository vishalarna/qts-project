import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FlyPanelIlaRegulatoryRequirementsComponent } from './fly-panel-ila-reg-requirements.component';


describe('FlyPanelAddTrainingProgramComponent', () => {
  let component: FlyPanelIlaRegulatoryRequirementsComponent;
  let fixture: ComponentFixture<FlyPanelIlaRegulatoryRequirementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelIlaRegulatoryRequirementsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelIlaRegulatoryRequirementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

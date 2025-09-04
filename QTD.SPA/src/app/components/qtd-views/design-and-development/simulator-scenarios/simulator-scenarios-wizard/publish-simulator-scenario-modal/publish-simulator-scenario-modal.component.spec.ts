import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublishSimulatorScenarioModalComponent } from './publish-simulator-scenario-modal.component';

describe('PublishSimulatorScenarioModalComponent', () => {
  let component: PublishSimulatorScenarioModalComponent;
  let fixture: ComponentFixture<PublishSimulatorScenarioModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublishSimulatorScenarioModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublishSimulatorScenarioModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

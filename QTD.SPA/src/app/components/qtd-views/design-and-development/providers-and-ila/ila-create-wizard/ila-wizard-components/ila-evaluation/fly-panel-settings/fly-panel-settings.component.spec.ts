import { FlyPanelSettingsComponent } from './fly-panel-settings.component';
import { ComponentFixture, TestBed } from '@angular/core/testing';


describe('FlyPanelIlaPrerequisitesComponent', () => {
  let component:  FlyPanelSettingsComponent;
  let fixture: ComponentFixture< FlyPanelSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [  FlyPanelSettingsComponent]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(  FlyPanelSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

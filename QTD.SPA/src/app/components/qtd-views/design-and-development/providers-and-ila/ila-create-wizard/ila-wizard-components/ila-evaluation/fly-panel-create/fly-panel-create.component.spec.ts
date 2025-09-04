import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FlyPanelCreateComponent } from './fly-panel-create.component';


describe('FlyPanelIlaPrerequisitesComponent', () => {
  let component:  FlyPanelCreateComponent;
  let fixture: ComponentFixture< FlyPanelCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [  FlyPanelCreateComponent]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(  FlyPanelCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

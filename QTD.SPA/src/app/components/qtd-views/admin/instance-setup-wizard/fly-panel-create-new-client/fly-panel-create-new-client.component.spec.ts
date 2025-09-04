import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FlyPanelCreateNewClientComponent } from './fly-panel-create-new-client.component';

describe('FlyPanelCreateNewClientComponent', () => {
  let component: FlyPanelCreateNewClientComponent;
  let fixture: ComponentFixture<FlyPanelCreateNewClientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCreateNewClientComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelCreateNewClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

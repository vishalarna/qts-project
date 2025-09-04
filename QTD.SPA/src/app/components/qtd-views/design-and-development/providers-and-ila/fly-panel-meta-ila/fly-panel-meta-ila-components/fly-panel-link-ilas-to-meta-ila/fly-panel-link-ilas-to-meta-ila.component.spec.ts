import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FlyPanelLinkILAsToMetaILAComponent } from './fly-panel-link-ilas-to-meta-ila.component';

describe('FlyPanelAddMetaILAEmployeesComponent', () => {
  let component: FlyPanelLinkILAsToMetaILAComponent;
  let fixture: ComponentFixture<FlyPanelLinkILAsToMetaILAComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkILAsToMetaILAComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkILAsToMetaILAComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

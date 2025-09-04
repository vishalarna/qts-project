import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FlyPanelOrganizationComponent } from './flyPanel-organizations.component';

describe('FlyPanelOrganizationComponent', () => {
  let component: FlyPanelOrganizationComponent;
  let fixture: ComponentFixture<FlyPanelOrganizationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FlyPanelOrganizationComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelOrganizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegRequirementsComponent } from './reg-requirements.component';

describe('RegRequirementsComponent', () => {
  let component: RegRequirementsComponent;
  let fixture: ComponentFixture<RegRequirementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegRequirementsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegRequirementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

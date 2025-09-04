import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoCategoryDetailsComponent } from './eo-category-details.component';

describe('EoCategoryDetailsComponent', () => {
  let component: EoCategoryDetailsComponent;
  let fixture: ComponentFixture<EoCategoryDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoCategoryDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoCategoryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

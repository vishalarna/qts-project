import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LabelReplacementComponent } from './label-replacement.component';

describe('LabelReplacementComponent', () => {
  let component: LabelReplacementComponent;
  let fixture: ComponentFixture<LabelReplacementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LabelReplacementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LabelReplacementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

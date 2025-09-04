import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NercComponent } from './nerc.component';

describe('NercComponent', () => {
  let component: NercComponent;
  let fixture: ComponentFixture<NercComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NercComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NercComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

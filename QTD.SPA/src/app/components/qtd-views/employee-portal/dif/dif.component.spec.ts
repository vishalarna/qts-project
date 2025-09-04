import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifComponent } from './dif.component';

describe('DifComponent', () => {
  let component: DifComponent;
  let fixture: ComponentFixture<DifComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

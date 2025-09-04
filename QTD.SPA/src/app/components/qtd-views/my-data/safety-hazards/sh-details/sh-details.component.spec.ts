import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShDetailsComponent } from './sh-details.component';

describe('ShDetailsComponent', () => {
  let component: ShDetailsComponent;
  let fixture: ComponentFixture<ShDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

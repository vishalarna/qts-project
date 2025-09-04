import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShRrComponent } from './sh-rr.component';

describe('ShRrComponent', () => {
  let component: ShRrComponent;
  let fixture: ComponentFixture<ShRrComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShRrComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShRrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

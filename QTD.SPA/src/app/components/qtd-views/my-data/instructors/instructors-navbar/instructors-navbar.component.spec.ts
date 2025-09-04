import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstructorsNavbarComponent } from './instructors-navbar.component';

describe('InstructorsNavbarComponent', () => {
  let component: InstructorsNavbarComponent;
  let fixture: ComponentFixture<InstructorsNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstructorsNavbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstructorsNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

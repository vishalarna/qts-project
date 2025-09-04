import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RosterPretestComponent } from './roster-pretest.component';

describe('RosterPretestComponent', () => {
  let component: RosterPretestComponent;
  let fixture: ComponentFixture<RosterPretestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RosterPretestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RosterPretestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RosterCbtComponent } from './roster-cbt.component';

describe('RosterCbtComponent', () => {
  let component: RosterCbtComponent;
  let fixture: ComponentFixture<RosterCbtComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RosterCbtComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RosterCbtComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

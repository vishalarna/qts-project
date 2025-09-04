import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RosterTestComponent } from './roster-test.component';

describe('RosterTestComponent', () => {
  let component: RosterTestComponent;
  let fixture: ComponentFixture<RosterTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RosterTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RosterTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelQuesBankComponent } from './fly-panel-ques-bank.component';

describe('FlyPanelQuesBankComponent', () => {
  let component: FlyPanelQuesBankComponent;
  let fixture: ComponentFixture<FlyPanelQuesBankComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelQuesBankComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelQuesBankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

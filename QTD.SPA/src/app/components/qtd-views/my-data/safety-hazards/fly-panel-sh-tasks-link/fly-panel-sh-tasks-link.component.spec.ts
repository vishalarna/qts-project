import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelShTasksLinkComponent } from './fly-panel-sh-tasks-link.component';

describe('FlyPanelShTasksLinkComponent', () => {
  let component: FlyPanelShTasksLinkComponent;
  let fixture: ComponentFixture<FlyPanelShTasksLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelShTasksLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelShTasksLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

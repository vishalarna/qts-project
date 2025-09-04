import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskIlaLinkComponent } from './fly-panel-task-ila-link.component';

describe('FlyPanelTaskIlaLinkComponent', () => {
  let component: FlyPanelTaskIlaLinkComponent;
  let fixture: ComponentFixture<FlyPanelTaskIlaLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskIlaLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskIlaLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddUserComponent } from './fly-panel-add-user.component';

describe('FlyPanelAddUserComponent', () => {
  let component: FlyPanelAddUserComponent;
  let fixture: ComponentFixture<FlyPanelAddUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

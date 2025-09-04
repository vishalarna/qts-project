import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddQtdUserComponent } from './fly-panel-create-qtd-user.component';

describe('FlyPanelAddQtdUserComponent', () => {
  let component: FlyPanelAddQtdUserComponent;
  let fixture: ComponentFixture<FlyPanelAddQtdUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddQtdUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddQtdUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

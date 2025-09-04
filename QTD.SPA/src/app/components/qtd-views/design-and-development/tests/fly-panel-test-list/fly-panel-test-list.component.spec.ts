import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTestListComponent } from './fly-panel-test-list.component';

describe('FlyPanelTestListComponent', () => {
  let component: FlyPanelTestListComponent;
  let fixture: ComponentFixture<FlyPanelTestListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTestListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTestListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

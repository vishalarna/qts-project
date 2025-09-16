import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddBasicIlaComponent } from './fly-panel-add-basic-ila.component';

describe('FlyPanelAddBasicIlaComponent', () => {
  let component: FlyPanelAddBasicIlaComponent;
  let fixture: ComponentFixture<FlyPanelAddBasicIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddBasicIlaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FlyPanelAddBasicIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

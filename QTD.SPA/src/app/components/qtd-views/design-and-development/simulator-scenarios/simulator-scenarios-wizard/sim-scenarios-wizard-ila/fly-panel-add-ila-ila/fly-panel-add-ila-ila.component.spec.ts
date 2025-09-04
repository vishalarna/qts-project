import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddIlaIlaComponent } from './fly-panel-add-ila-ila.component';

describe('FlyPanelAddIlaIlaComponent', () => {
  let component: FlyPanelAddIlaIlaComponent;
  let fixture: ComponentFixture<FlyPanelAddIlaIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddIlaIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddIlaIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

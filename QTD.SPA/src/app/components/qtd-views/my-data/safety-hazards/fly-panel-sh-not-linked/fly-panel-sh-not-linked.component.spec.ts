import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelShNotLinkedComponent } from './fly-panel-sh-not-linked.component';

describe('FlyPanelShNotLinkedComponent', () => {
  let component: FlyPanelShNotLinkedComponent;
  let fixture: ComponentFixture<FlyPanelShNotLinkedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelShNotLinkedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelShNotLinkedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

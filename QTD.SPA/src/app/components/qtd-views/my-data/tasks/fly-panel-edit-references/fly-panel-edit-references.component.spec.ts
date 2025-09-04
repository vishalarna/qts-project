import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditReferencesComponent } from './fly-panel-edit-references.component';

describe('FlyPanelEditReferencesComponent', () => {
  let component: FlyPanelEditReferencesComponent;
  let fixture: ComponentFixture<FlyPanelEditReferencesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditReferencesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditReferencesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTestResultComponent } from './fly-panel-test-result.component';

describe('FlyPanelTestResultComponent', () => {
  let component: FlyPanelTestResultComponent;
  let fixture: ComponentFixture<FlyPanelTestResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTestResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTestResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEOTopicComponent } from './flypanel-eo-topic.component';

describe('FlypanelEOTopicComponent', () => {
  let component: FlypanelEOTopicComponent;
  let fixture: ComponentFixture<FlypanelEOTopicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEOTopicComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEOTopicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

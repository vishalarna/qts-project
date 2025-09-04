import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoTopicDetailsComponent } from './eo-topic-details.component';

describe('EoTopicDetailsComponent', () => {
  let component: EoTopicDetailsComponent;
  let fixture: ComponentFixture<EoTopicDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoTopicDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoTopicDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

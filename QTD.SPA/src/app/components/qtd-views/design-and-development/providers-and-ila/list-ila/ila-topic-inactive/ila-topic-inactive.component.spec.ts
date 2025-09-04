import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaTopicInactiveComponent } from './ila-topic-inactive.component';

describe('IlaTopicInactiveComponent', () => {
  let component: IlaTopicInactiveComponent;
  let fixture: ComponentFixture<IlaTopicInactiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaTopicInactiveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaTopicInactiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaTopicDeleteComponent } from './ila-topic-delete.component';

describe('IlaTopicDeleteComponent', () => {
  let component: IlaTopicDeleteComponent;
  let fixture: ComponentFixture<IlaTopicDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaTopicDeleteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaTopicDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

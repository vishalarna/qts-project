import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviewAndPublishComponent } from './preview-and-publish.component';

describe('PreviewAndPublishComponent', () => {
  let component: PreviewAndPublishComponent;
  let fixture: ComponentFixture<PreviewAndPublishComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PreviewAndPublishComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PreviewAndPublishComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

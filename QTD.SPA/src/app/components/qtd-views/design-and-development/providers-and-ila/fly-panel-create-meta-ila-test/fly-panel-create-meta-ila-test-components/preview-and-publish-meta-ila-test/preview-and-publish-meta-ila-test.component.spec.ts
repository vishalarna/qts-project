import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviewAndPublishMetaILATestComponent } from './preview-and-publish-meta-ila-test.component';

describe('PreviewAndPublishMetaILATestComponent', () => {
  let component: PreviewAndPublishMetaILATestComponent;
  let fixture: ComponentFixture<PreviewAndPublishMetaILATestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PreviewAndPublishMetaILATestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PreviewAndPublishMetaILATestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

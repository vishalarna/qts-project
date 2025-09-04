import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConclusionAndActionItemsComponent } from './conclusion-and-action-items.component';

describe('ConclusionAndActionItemsComponent', () => {
  let component: ConclusionAndActionItemsComponent;
  let fixture: ComponentFixture<ConclusionAndActionItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConclusionAndActionItemsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConclusionAndActionItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TreeItemSelectorComponent } from './tree-item-selector.component';

describe('TreeItemSelectorComponent', () => {
  let component: TreeItemSelectorComponent;
  let fixture: ComponentFixture<TreeItemSelectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TreeItemSelectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TreeItemSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

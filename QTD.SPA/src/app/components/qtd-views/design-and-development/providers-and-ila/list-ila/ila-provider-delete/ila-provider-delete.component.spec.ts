import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaProviderDeleteComponent } from './ila-provider-delete.component';

describe('IlaProviderDeleteComponent', () => {
  let component: IlaProviderDeleteComponent;
  let fixture: ComponentFixture<IlaProviderDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaProviderDeleteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaProviderDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

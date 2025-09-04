import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaProviderActiveComponent } from './ila-provider-active.component';

describe('IlaProviderActiveComponent', () => {
  let component: IlaProviderActiveComponent;
  let fixture: ComponentFixture<IlaProviderActiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaProviderActiveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaProviderActiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

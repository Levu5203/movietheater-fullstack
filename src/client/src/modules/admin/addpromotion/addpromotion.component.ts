import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCamera, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { PromotionService } from '../../../services/promotion/promotion-service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-addpromotion',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule, FormsModule],
  templateUrl: './addpromotion.component.html',
  styleUrl: './addpromotion.component.css'
})
export class AddpromotionComponent {
  public faCamera: IconDefinition = faCamera;
  public showErrorMessage: boolean = false;
  public errorMessage: string = '';
  
  promotion = {
    promotionTitle: '',
    description: '',
    discount: 0,
    startDate: '',
    endDate: ''
  };
  selectedFile: File | null = null;
  imagePreviewUrl: string | ArrayBuffer | null = null;

  constructor(private promotionService: PromotionService, private router: Router) {}

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file && file.type.startsWith('image/')) {
      this.selectedFile = file;
      
      // Create preview for the selected image
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreviewUrl = reader.result;
      };
      reader.readAsDataURL(file);
    } else {
      alert('Please select a valid image file');
      this.selectedFile = null;
      this.imagePreviewUrl = null;
    }
  }

  onSubmit() {
    event?.preventDefault(); // Prevent page reload when submitting form
  
    // Validate promotion title and description
    if (!this.promotion.promotionTitle.trim() || !this.promotion.description.trim()) {
      this.showErrorMessage = true;
      this.errorMessage = 'Please fill in all required fields';
      return;
    }
  
    // Check if discount is a positive number
    const discount = Number(this.promotion.discount);
    if (isNaN(discount) || discount <= 0) {
      this.showErrorMessage = true;
      this.errorMessage = 'Discount must be a positive number';
      return;
    }
  
    // Check if dates are provided
    if (!this.promotion.startDate || !this.promotion.endDate) {
      this.showErrorMessage = true;
      this.errorMessage = 'Please select both start and end dates';
      return;
    }
  
    // Check if endDate is not before startDate
    const startDate = new Date(this.promotion.startDate);
    const endDate = new Date(this.promotion.endDate);
    if (endDate < startDate) {
      this.showErrorMessage = true;
      this.errorMessage = 'End date cannot be before start date';
      return;
    }
  
    // Check if image is provided
    if (!this.selectedFile) {
      this.showErrorMessage = true;
      this.errorMessage = 'Please upload an image for the promotion';
      return;
    }
  
    const formData = new FormData();
    formData.append('PromotionTitle', this.promotion.promotionTitle);
    formData.append('Description', this.promotion.description);
    formData.append('Discount', (this.promotion.discount / 100).toString());
    formData.append('StartDate', this.promotion.startDate);
    formData.append('EndDate', this.promotion.endDate);
    
    if (this.selectedFile) {
      formData.append('Image', this.selectedFile);
    }
  
    console.log(formData);
  
    this.promotionService.createPromotion(formData).subscribe({
      next: () => {
        this.router.navigate(['admin/promotionmanagement']);
      },
      error: (err) => {
        console.error('Error creating promotion:', err);
        this.showErrorMessage = true;
        this.errorMessage = err.error?.message || 'Failed to create promotion. Please try again.';
      }
    });
  }

  cancel() {
    this.router.navigate(['admin/promotionmanagement']);
  }
}
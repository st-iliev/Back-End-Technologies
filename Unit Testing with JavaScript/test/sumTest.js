import { expect } from 'chai';

import { sum } from './sum.js';

describe('The function sum', () => {

    it('should return 0 if an empty array is given', () => {
    // Arrange
        const array = [];
    // Act
        let result = sum(array);
    // Assert
         expect(result).to.equals(0);

    });

    it('should return the single element as a sum  if a single element array is given', () => {
    // Arrange
        const array = [13];
    // Act
        let result = sum(array);
    // Assert
        expect(result).to.equals(13);
    });

    it('should return the total sum of an array of multi value array is given', () => {
    // Arrange
        const array = [1,2,3];
    // Act
        let result = sum(array);
    // Assert
        expect(result).to.equals(6);
    
    });

});
import { expect } from 'chai';

import { isSymmetric } from './symmetry.js';

describe('Test of isSymmetric', () => {
    it('if given a empty array should return false', ()=> {
        // Arrange
        const array = [];
        // Act
        let result = isSymmetric(array);
        // Assert
        expect(result).to.be.true;
    });
    it('if given a incorrect type should return false', () => {
        // Arrange
        const array = 'waka waka';
        // Act
        let result = isSymmetric(array);
        // Assert
        expect(result).to.be.false;
    });
    it('if given a symmetric array should return true', () => {
        // Arrange
        const array = [2,3,2];
        // Act
        let result = isSymmetric(array);
        // Assert
        expect(result).to.be.true;
    });
    it('if given a non-symmetric array should return false', () => {
        // Arrange
        const array = [1,3,2];
        // Act
        let result = isSymmetric(array);
        // Assert
        expect(result).to.be.false;
    });
    it('should return false for symmetric lookalike values', () => {
        // Arrange
        const array = ['1','2','3',2,1];
        // Act
        let result = isSymmetric(array);
        // Assert
        expect(result).to.be.false;
    });
});
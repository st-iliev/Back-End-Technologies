import { createCalculator } from './addSubstract.js'
import { expect } from 'chai'

describe('Test function addSubstract', () => {
    it('should return 0 if no operation are executed on the caculator', () => {
        // Arrange
        const calculator = createCalculator();
        // Act
        let result = calculator.get();
        // Assert
        expect(result).to.equals(0);
    })
    it('should return a positive number if only subtract operation is executed with negative numbers on the caculator', () => {
        // Arrange
        const calculator = createCalculator();
        // Act
        calculator.subtract(-5)
        const result = calculator.get();
        // Assert
        expect(result).to.equals(5);
    })
    it('should return a negative number if only add operation is executed with positive numbers on the calculator', () => {
        // Arrange
        const calculator = createCalculator();
        // Act
        calculator.subtract(10)
        calculator.subtract(25)
        const result = calculator.get();
        // Assert
        expect(result).to.equals(-35);
    })
    it('should handle number as string', () => {
        // Arrange
        const calculator = createCalculator();
        // Act
        calculator.add('10')
        calculator.add('25')
        calculator.subtract('35')
        const result = calculator.get();
        // Assert
        expect(result).to.equals(0);
    })
    it('should handle a mix of operations', () => {
         // Arrange
         const calculator = createCalculator();
         // Act
         calculator.add(10)
         calculator.subtract(5)
         const result = calculator.get();
         // Assert
         expect(result).to.equals(5);
    })
})
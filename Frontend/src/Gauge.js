import React from 'react';

const Gauge = ({ min, max, value }) => {
  const degreeRatio = 180 / (max - min);
  const deg = (value - min) * degreeRatio;
  const rad = deg * Math.PI / 180;
  const x = Math.sin(rad);
  const y = Math.cos(rad);
  const mid = (max - min) / 2;
  const newValue = value > mid ? value - mid : mid - value;
  const newDegreeRatio = 180 / (mid - min);
  const newDeg = (newValue - min) * newDegreeRatio;
  const newRad = newDeg * Math.PI / 180;
  const newX = Math.sin(newRad);
  const newY = Math.cos(newRad);
  const length = 60;
  const y1 = -length * y;
  const y2 = length * y;
  const x1 = -length * x;
  const x2 = length * x;
  const newY1 = -length * newY;
  const newY2 = length * newY;
  const newX1 = -length * newX;
  const newX2 = length * newX;

  return (
    <svg width="100" height="100">
      <line x1="50" y1="10" x2="50" y2="90" stroke="#ccc" strokeWidth="20" strokeLinecap="round" />
      <line x1="50" y1="50" x2={50 + x1} y2={50 + y1} stroke="#f00" strokeWidth="20" strokeLinecap="round" />
      <line x1={50 + x1} y1={50 + y1} x2={50 + x2} y2={50 + y2} stroke="#ccc" strokeWidth="20" strokeLinecap="round" />
      <line x1={50 + newX1} y1={50 + newY1} x2={50 + newX2} y2={50 + newY2} stroke="#f00" strokeWidth="20" strokeLinecap="round" />
    </svg>
  );
};

export default Gauge;
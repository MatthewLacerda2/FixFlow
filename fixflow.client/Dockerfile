# Use the official Node.js image
FROM node:latest AS build
WORKDIR /app

# Copy package.json and install dependencies
COPY package*.json ./
RUN npm install

# Copy the entire application
COPY . .

# Build the React app
RUN npm run build

# Use a smaller image for serving the app
FROM node:alpine AS runtime
WORKDIR /app

# Copy the built files from the previous stage
COPY --from=build /app/build .

# Expose the port your application will run on
EXPOSE 5173

# Install serve to run the app
RUN npm install -g serve

# Define the entry point for the application
CMD ["serve", "-s", "."]

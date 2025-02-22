# Variables
$project_id = "campus-system-451705"
$region = "asia-east1"
$image_name = "asia-east1-docker.pkg.dev/campus-system-451705/campus-system-dev/campus-system-dev:latest"
$dockerfile_name = "Dockerfile"
$service_name = "campus-system-dev"
$service_account = "749888051547-compute@developer.gserviceaccount.com"

# Step 1: Set GCP Project
Write-Host "[Step-1] Setting GCP project to '$project_id'..."
gcloud config set project $project_id

if ($LASTEXITCODE -ne 0) {
    Write-Host "[Step-1] Failed to set GCP project. Exiting script." -ForegroundColor Red
    exit $LASTEXITCODE
} else {
    Write-Host "[Step-1] Successfully set GCP project to '$project_id'." -ForegroundColor Green
}

# Step 2: Authenticate Docker with GCP Artifact Registry
Write-Host "[Step-2] Authenticating Docker with GCP Artifact Registry in region '$region'..."
gcloud auth configure-docker $region-docker.pkg.dev

if ($LASTEXITCODE -ne 0) {
    Write-Host "[Step-2] Docker authentication with GCP Artifact Registry failed. Exiting script." -ForegroundColor Red
    exit $LASTEXITCODE
} else {
    Write-Host "[Step-2] Successfully authenticated Docker with GCP Artifact Registry." -ForegroundColor Green
}

# Step 3: Build the Docker image
Write-Host "[Step-3] Building Docker image '$image_name' using Dockerfile '$dockerfile_name'..."
docker build -t $image_name -f $dockerfile_name .

if ($LASTEXITCODE -ne 0) {
    Write-Host "[Step-3] Docker image build failed. Exiting script." -ForegroundColor Red
    exit $LASTEXITCODE
} else {
    Write-Host "[Step-3] Successfully built Docker image '$image_name'." -ForegroundColor Green
}

# Step 4: Push the Docker image to Artifact Registry
Write-Host "[Step-4] Pushing Docker image '$image_name' to Artifact Registry..."
docker push $image_name

if ($LASTEXITCODE -ne 0) {
    Write-Host "[Step-4] Failed to push Docker image to Artifact Registry. Exiting script." -ForegroundColor Red
    exit $LASTEXITCODE
} else {
    Write-Host "[Step-4] Successfully pushed Docker image '$image_name' to Artifact Registry." -ForegroundColor Green
}

# Step 5: Deploy the Docker image to Cloud Run
Write-Host "[Step-5] Deploying Docker image '$image_name' to Cloud Run service '$service_name'..."
gcloud run deploy $service_name `
    --image $image_name `
    --platform managed `
    --region $region `
    --port 80 `
    --cpu 1 `
    --memory 256Mi `
    --max-instances 1 `
    --timeout 300s `
    --service-account $service_account `
    --concurrency 10 `
    --set-env-vars ASPNETCORE_ENVIRONMENT=Development `
    --allow-unauthenticated

if ($LASTEXITCODE -ne 0) {
    Write-Host "[Step-5] Cloud Run deployment failed. Exiting script." -ForegroundColor Red
    exit $LASTEXITCODE
} else {
    Write-Host "[Step-5] Successfully deployed Docker image to Cloud Run service '$service_name'." -ForegroundColor Green
}

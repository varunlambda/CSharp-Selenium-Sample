

sudo chmod +x ./dotnet-install.sh

./dotnet-install.sh --version latest

export DOTNET_ROOT=/usr/share/dotnet/
export PATH=$PATH:$DOTNET_ROOT
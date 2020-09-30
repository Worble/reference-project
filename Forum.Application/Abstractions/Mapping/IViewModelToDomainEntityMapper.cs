using Forum.Application.Abstractions.Models;
using Forum.Domain.Abstractions.Models;

namespace Forum.Application.Abstractions.Mapping
{
	public interface IViewModelToDomainEntityMapper<TViewModel, TDomainEntity>
		where TViewModel : IViewModel
		where TDomainEntity : IDomainEntity
	{
		TDomainEntity MapToDomainEntity(TViewModel viewModel);

		TViewModel MapToViewModel(TDomainEntity domainEntity);
	}
}

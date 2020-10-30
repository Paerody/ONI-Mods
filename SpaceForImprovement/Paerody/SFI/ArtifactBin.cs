using System.Collections.Generic;
using KSerialization;
using UnityEngine;

namespace Paerody.SFI
{
	class ArtifactBin : KMonoBehaviour, IUserControlledCapacity
	{
		protected override void OnPrefabInit()
		{
			this.Initialize(false);
		}

		protected void Initialize(bool use_logic_meter)
		{
			base.OnPrefabInit();
			this.log = new LoggerFS("StorageLocker", 35);
			this.filteredStorage = new FilteredStorage(this, null, null, this, use_logic_meter, Db.Get().ChoreTypes.StorageFetch);
			this.filteredStorage.filterTint = Color.green;
			this.filteredStorage.noFilterTint = Color.red;
			base.Subscribe<ArtifactBin>(ArtifactBinConfig.ID.GetHashCode(), ArtifactBin.OnCopySettingsDelegate);
			foreach (string artifact in ArtifactConfig.artifactItems)
			{
				WorldInventory.Instance.Discover(artifact.ToTag(), GameTags.Miscellaneous);
			}
			Strings.Add($"STRINGS.MISC.TAGS.ARTIFACT", "Artifacts");
		}

		protected override void OnSpawn()
		{
			this.filteredStorage.FilterChanged();
			if (this.nameable != null && !this.lockerName.IsNullOrWhiteSpace())
			{
				this.nameable.SetName(this.lockerName);
			}
		}

		protected override void OnCleanUp()
		{
			//this.filteredStorage.CleanUp();
		}

		private void OnCopySettings(object data)
		{
			GameObject gameObject = (GameObject)data;
			if (gameObject == null)
			{
				return;
			}
			ArtifactBin component = gameObject.GetComponent<ArtifactBin>();
			if (component == null)
			{
				return;
			}
			this.UserMaxCapacity = component.UserMaxCapacity;
		}

		public virtual float UserMaxCapacity
		{
			get { return Mathf.Min(this.userMaxCapacity, base.GetComponent<Storage>().capacityKg); }
			set
			{
				this.userMaxCapacity = value;
				this.filteredStorage.FilterChanged();
			}
		}

		public float AmountStored
		{
			get { return base.GetComponent<Storage>().MassStored(); }
		}

		public float MinCapacity
		{
			get { return 0f; }
		}

		public float MaxCapacity
		{
			get { return base.GetComponent<Storage>().capacityKg; }
		}

		public bool WholeValues 
		{ 
			get { return true; } 
		}

		public LocString CapacityUnits 
		{ 
			get { return GameUtil.GetCurrentMassUnit(false); } 
		}

		private LoggerFS log;

		[Serialize]
		private float userMaxCapacity = float.PositiveInfinity;

		[Serialize]
		public string lockerName = "";

		protected FilteredStorage filteredStorage;

		[MyCmpGet]
		private UserNameable nameable;

		private static readonly EventSystem.IntraObjectHandler<ArtifactBin> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<ArtifactBin>(delegate (ArtifactBin component, object data)
		{
			component.OnCopySettings(data);
		});
	}
}
